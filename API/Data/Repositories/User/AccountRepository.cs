using API.DTOs.Users;
using API.Entities;
using API.Interfaces.Users;
using AutoMapper;
using ErrorOr;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories.User;

public class AccountRepository : IAccountRepository
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;

    public AccountRepository(UserManager<AppUser> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<ErrorOr<LoginDto>> RegisterAsync(RegisterRequest request)
    {
        if (await UserNameExists(request.Username!))
            return Error.Conflict("User.DuplicateUsername", $"Username '{request.Username}' is already taken.");

        // var user = new AppUser
        // {
        //     Firstname = request.Firstname!,
        //     Lastname = request.Lastname!,
        //     UserName = request.Username?.ToLower(),
        //     Gender = request.Gender!,
        //     Email = request.Email?.Trim().ToLower()
        // };

        var user = _mapper.Map<AppUser>(request);

        var userResult = await _userManager.CreateAsync(user, request.Password);
        if (!userResult.Succeeded)
        {
            var errors = userResult.Errors.Select(e => e.Description).ToArray();
            return Error.Failure("User.IdentityError", string.Join("\n", errors));
        }

        var roleResult = await _userManager.AddToRoleAsync(user, RolePermission.Member);
        if (!roleResult.Succeeded)
        {
            var errors = roleResult.Errors.Select(e => e.Description).ToArray();
            return Error.Failure("User.IdentityError", string.Join("\n", errors));
        }

        return new LoginDto
        {
            Username = user.UserName!,
            Gender = user.Gender,
            Fullname = user.Fullname
        };
    }

    private async Task<bool> UserNameExists(string username)
    {
        return await _userManager.Users.AnyAsync(u => u.UserName == username);
    }
}
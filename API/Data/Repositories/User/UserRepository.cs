using System;
using API.DTOs.Users;
using API.Extensions;
using API.Interfaces.Users;
using API.Utilities;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories.User;

public class UserRepository : IUserRepository
{
    private readonly MingleDbContext _dbcontext;
    public UserRepository(MingleDbContext dbcontext)
    {
        _dbcontext = dbcontext;
    }

    public async Task<List<UserDto>> GetAllUsersAsync(UserParams usersParams)
    {
        var query = _dbcontext.Users.AsQueryable();

        query = query.Where(u => u.UserName != usersParams.CurrentUsername);
        query = query.Where(u => u.Gender == usersParams.Gender);

        var minDob = DateTime.Today.AddYears(-usersParams.MaxAge - 1);
        var maxDob = DateTime.Today.AddYears(-usersParams.MinAge);

        query = query.Where(u => u.DateOfBirth >= minDob && u.DateOfBirth <= maxDob);

        // switch (usersParams.OrderBy)
        // {
        //     case "created":
        //         query = query.OrderByDescending(u => u.DateCreated);
        //         break;
        //     default:
        //         query = query.OrderByDescending(u => u.LastActive);
        //         break;
        // }

        query = usersParams.OrderBy switch
        {
            "created" => query.OrderByDescending(u => u.DateCreated),
            _ => query.OrderByDescending(u => u.LastActive)
        };

        return await query
            .Select(a => new UserDto {
                Username = a.UserName,
                Fullname = a.Fullname,
                MainPhoto = a.Photos!.FirstOrDefault(p => p.IsMain)!.Url,
                Gender = a.Gender,
                Age = a.DateOfBirth.GetAge(),
                LastActive = a.LastActive,
                Created = a.DateCreated
            })
            .AsNoTracking()
            .ToListAsync();
    }
}
using API.DTOs.Users;
using API.Interfaces.Users;
using API.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")] // => /users
public class UsersController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    public UsersController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<UserDto>>> GetAllUsers([FromQuery] UserParams userParams)
    {
        var userDto = await _userRepository.GetAllUsersAsync(userParams);

        return Ok(userDto);
    }
}
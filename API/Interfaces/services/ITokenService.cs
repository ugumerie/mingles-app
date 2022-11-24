using API.Entities;

namespace API.Interfaces.services;

public interface ITokenService
{
    Task<string> GenerateAccessTokenAsync(AppUser user);
}
using UdemusDateus.Entities;

namespace UdemusDateus.Interfaces;

public interface ITokenService
{
    Task<string> CreateToken(AppUser user);
}
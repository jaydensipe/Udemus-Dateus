using UdemusDateus.Entities;

namespace UdemusDateus.Interfaces;

public interface ITokenService
{
    string CreateToken(AppUser user);
}
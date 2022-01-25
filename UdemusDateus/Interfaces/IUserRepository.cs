using UdemusDateus.DTOs;
using UdemusDateus.Entities;

namespace UdemusDateus.Interfaces;

public interface IUserRepository
{
    void Update(AppUser user);
    Task<bool> SaveAllAsync();
    Task<IEnumerable<MemberDto>> GetMembersAsync();
    Task<MemberDto> GetMemberByUserNameAsync(string userName);
}
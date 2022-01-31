using UdemusDateus.DTOs;
using UdemusDateus.Entities;
using UdemusDateus.Helpers;

namespace UdemusDateus.Interfaces;

public interface IUserRepository
{
    void Update(AppUser user);
    Task<bool> SaveAllAsync();
    Task<AppUser> GetUserByIdAsync(int id);
    Task<IEnumerable<AppUser>> GetUsersAsync();
    Task<AppUser> GetUserByUserNameAsync(string userName);
    Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams);
    Task<MemberDto> GetMemberByUserNameAsync(string userName);
}
using UdemusDateus.DTOs;
using UdemusDateus.Entities;
using UdemusDateus.Helpers;

namespace UdemusDateus.Interfaces;

public interface ILikesRepository
{
    Task<UserLike> GetUserLike(int sourceUserId, int intLikedUserId);
    Task<AppUser> GetUserWithLikes(int userId);
    Task<PagedList<LikeDto>> GetUserLikes(LikesParams likesParams);
}
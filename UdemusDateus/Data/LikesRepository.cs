using Microsoft.EntityFrameworkCore;
using UdemusDateus.DTOs;
using UdemusDateus.Entities;
using UdemusDateus.Extensions;
using UdemusDateus.Helpers;
using UdemusDateus.Interfaces;

namespace UdemusDateus.Data;

public class LikesRepository : ILikesRepository
{
    private readonly DataContext _context;

    public LikesRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<UserLike> GetUserLike(int sourceUserId, int likedUserId)
    {
        return await _context.Likes.FindAsync(sourceUserId, likedUserId);
    }

    public async Task<AppUser> GetUserWithLikes(int userId)
    {
        return await _context.Users.Include(user => user.LikedUsers).FirstOrDefaultAsync(user => user.Id == userId);
    }

    public async Task<PagedList<LikeDto>> GetUserLikes(LikesParams likesParams)
    {
        var users = _context.Users.OrderBy(user => user.UserName).AsQueryable();
        var likes = _context.Likes.AsQueryable();

        switch (likesParams.Predicate)
        {
            case "liked":
                likes = likes.Where(like => like.SourceUserId == likesParams.UserId);
                users = likes.Select(like => like.LikedUser);
                break;
            case "likedBy":
                likes = likes.Where(like => like.LikedUserId == likesParams.UserId);
                users = likes.Select(like => like.SourceUser);
                break;
        }

        var likedUsers = users.Select(user => new LikeDto
        {
            UserName = user.UserName,
            ScreenName = user.ScreenName,
            Age = user.DateOfBirth.CalculateAge(),
            City = user.City,
            Id = likesParams.UserId,
            PhotoUrl = user.Photos.FirstOrDefault(p => p.IsMain).Url
        });

        return await PagedList<LikeDto>.CreateAsync(likedUsers, likesParams.PageNumber, likesParams.PageSize);
    }
}
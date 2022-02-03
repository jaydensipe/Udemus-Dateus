using Microsoft.AspNetCore.Identity;

namespace UdemusDateus.Entities;

public class AppUser : IdentityUser<int>
{ 
    public DateTime DateOfBirth { get; set; }
    public string ScreenName { get; set; }

    public DateTime ProfileCreated { get; set; } = DateTime.Now;

    public DateTime LastActive { get; set; } = DateTime.Now;

    public string Gender { get; set; }

    public string Introduction { get; set; }

    public string LookingFor { get; set; }

    public string Interests { get; set; }

    public string City { get; set; }

    public string Country { get; set; }
    public ICollection<Photo> Photos { get; set; }
    public ICollection<UserLike> LikedByUsers { get; set; }
    public ICollection<UserLike> LikedUsers { get; set; }

    public ICollection<Message> MessagesSent { get; set; }
    public ICollection<Message> MessagesReceived { get; set; }
    
    public ICollection<AppUserRole> UserRoles { get; set; }

}
using Microsoft.AspNetCore.Identity;

namespace UdemusDateus.Entities;

public class AppRole : IdentityRole<int>
{
    public ICollection<AppUserRole> UserRoles { get; set; }
}
using Microsoft.EntityFrameworkCore;
using UdemusDateus.Entities;

namespace UdemusDateus.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<AppUser> Users { get; set; }
}
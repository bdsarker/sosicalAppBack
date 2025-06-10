using api.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Data;

public class AppDbContext:DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<AppUser> Users { get; set; } = null!;
    public DbSet<Member> Members { get; set; }
    public DbSet<Photo> Photos { get; set; }
}
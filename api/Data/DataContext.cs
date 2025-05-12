using api.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Data;

public class DataContext:DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }

    public DbSet<AppUser> Users { get; set; } = null!;
}
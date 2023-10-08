using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Sparks.Api.Features.Users;

namespace Sparks.Api.Database;

public class SparksDbContext : DbContext
{
    public SparksDbContext(DbContextOptions<SparksDbContext> options) : base(options)
    {
        
    }

    public DbSet<User> Users => Set<User>();
}
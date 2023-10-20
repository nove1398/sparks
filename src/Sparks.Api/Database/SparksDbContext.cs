using Microsoft.EntityFrameworkCore;
using Postgrest;
using Sparks.Api.Features.Interests;
using Sparks.Api.Features.Messaging;
using Sparks.Api.Features.Rooms;
using Sparks.Api.Features.Users;

namespace Sparks.Api.Database;

public class SparksDbContext : DbContext
{
    public SparksDbContext(DbContextOptions<SparksDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<ClientOptions>();
        modelBuilder.Ignore<UserPhoto>();
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Program).Assembly);
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Message> Messages => Set<Message>();
    public DbSet<Room> Rooms => Set<Room>();
    public DbSet<Interest> Interests => Set<Interest>();
}
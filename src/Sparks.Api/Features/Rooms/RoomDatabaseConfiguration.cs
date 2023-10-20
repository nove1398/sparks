using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sparks.Api.Features.Rooms;

public class RoomDatabaseConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(250).IsRequired();
        builder.Property(x => x.ActualStartUtc).IsRequired();
        builder.Property(x => x.ActualEndUtc).IsRequired(false);
        builder.Property(x => x.Venue).IsRequired(false);
        builder.Property(x => x.Capacity).IsRequired();
        builder.Property(x => x.HostUserId).IsRequired();
        builder.Property(x => x.MatchedUserId).IsRequired();
        builder.Property(x => x.PrivacyType).HasConversion<string>();
    }
}
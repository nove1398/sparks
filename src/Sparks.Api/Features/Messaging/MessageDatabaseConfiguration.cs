using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sparks.Api.Features.Messaging;

public class MessageDatabaseConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.SenderId).IsRequired();
        builder.Property(x => x.ReceiverId).IsRequired();
        builder.Property(x => x.RoomId).IsRequired();
        builder.Property(x => x.Content).IsRequired(false).HasMaxLength(1000);
        builder.Property(x => x.CanExpire).IsRequired().HasDefaultValue(false);
    }
}
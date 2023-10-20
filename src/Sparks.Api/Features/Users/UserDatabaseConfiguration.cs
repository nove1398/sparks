using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sparks.Api.Features.Users;

public class UserDatabaseConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.UserName).IsRequired();
        builder.Property(x => x.DisplayName).IsRequired();
        builder.Property(x => x.Country).IsRequired();
        builder.Property(x => x.Email).IsRequired();
        builder.Property(x => x.HeightInCm).IsRequired();
        builder.Property(x => x.IsAccountActive).IsRequired();
        builder.Property(x => x.Age).IsRequired();
        builder.Property(x => x.Gender).HasConversion<string>();
        builder.Property(x => x.StarSign).HasConversion<string>();
        builder.HasMany(x => x.Interests)
                .WithMany(interests => interests.Users);
        builder.OwnsMany<UserPhoto>(x => x.UserPhotos);
    }
}
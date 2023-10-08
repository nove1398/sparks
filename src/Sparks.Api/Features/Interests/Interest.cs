using Sparks.Api.Features.Users;
using Sparks.Api.Shared;

namespace Sparks.Api.Features.Interests;

public class Interest : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public ICollection<User> Users { get; private set; } = new List<User>();
    private Interest()
    {
        
    }

    public static Interest Create(string name)
    {
        return new Interest()
        {
            Id = Guid.NewGuid(),
            CreatedAtUtc = DateTime.UtcNow,
            Name = name
        };
    }
}
using Sparks.Api.Shared;

namespace Sparks.Api.Features.Rooms;

public class Room : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public TimeOnly SessionDuration { get; private set; }
    public DateTime ActualStartUtc { get; private set; }
    public DateTime? ActualEndUtc { get; private set; }
    public PrivacyTypes PrivacyType { get; private set; }
    public string? Venue { get; private set; }
    public int Capacity { get; private set; }
    public Guid HostUserId { get; private set; }
    public Guid MatchedUserId { get; private set; }
    private Room()
    {
        
    }

    public static Room Create(string name, Guid hostId, Guid matchedUserId, int capacity, PrivacyTypes privacyType, string? venue, int validForMinutes)
    {
        var newRoom = new Room()
        {
            Id = Guid.NewGuid(),
            CreatedAtUtc = DateTime.UtcNow,
            ActualStartUtc = DateTime.UtcNow,
            PrivacyType = privacyType,
            Capacity = capacity,
            HostUserId = hostId,
            MatchedUserId = matchedUserId,
            Venue = venue,
            Name = name,
            SessionDuration = TimeOnly.FromDateTime(DateTime.UtcNow).AddMinutes(validForMinutes)
        };
        
        return newRoom;
    }
}



public enum PrivacyTypes
{
    Public = 1,
    Private = 2,
    InviteOnly = 3,
    Matched = 4
}
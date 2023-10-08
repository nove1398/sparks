using Postgrest;
using Sparks.Api.Shared;

namespace Sparks.Api.Features.Messaging;

public class Message : BaseEntity
{
    public Guid SenderId { get; private set; }
    public Guid ReceiverId { get; private set; }
    public Guid RoomId { get; private set; }
    public bool CanExpire { get; private set; }
    public string? Content { get; private set; }
    private Message(){}


    public static Message Create(Guid senderId, Guid receiverId, Guid roomId, string? content, bool canExpire = false)
    {
        var newMessage = new Message()
        {
            Id = Guid.NewGuid(),
            CreatedAtUtc = DateTime.UtcNow,
            CanExpire = canExpire,
            SenderId = senderId,
            ReceiverId = receiverId,
            RoomId = roomId,
            Content = content
        };
        return newMessage;
    }
}
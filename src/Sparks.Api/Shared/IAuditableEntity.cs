namespace Sparks.Api.Shared;

public interface IAuditableEntity
{
    public DateTime ModifiedDateUtc { get; set; }
    public string ModifiedById { get; set; }
}
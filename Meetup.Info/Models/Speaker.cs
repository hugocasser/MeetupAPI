namespace Meetup.Info.Models;

public class Speaker
{
    public required Guid Id { get; set; }
    public required Guid ExternalId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required ICollection<EventInformation> Events { get; set; } = new List<EventInformation>();
}
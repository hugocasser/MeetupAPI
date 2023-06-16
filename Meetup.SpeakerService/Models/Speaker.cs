namespace Meetup.SpeakerService.Models;

public class Speaker
{
    public required Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? Presentation { get; set; }
}
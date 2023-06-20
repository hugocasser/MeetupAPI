namespace Meetup.Application.Dtos;

public class CreateSpeakerDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string? Presentation { get; set; }
}
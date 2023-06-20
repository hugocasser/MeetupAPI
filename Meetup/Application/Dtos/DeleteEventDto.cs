namespace Meetup.Application.Dtos;

public class DeleteEventDto
{
    public required Guid SpeakerId { get; set; }
    public required Guid Id { get; set; }
}
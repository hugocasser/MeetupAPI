namespace Meetup.Info.Application.DTOs;

public class SpeakerDeleteDTO
{
    public required Guid Id { get; set; }
    public required string EventMQ { get; set; }
}
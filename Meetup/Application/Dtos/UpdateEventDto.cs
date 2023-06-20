namespace Meetup.Application.Dtos;

public class UpdateEventDto
{
    public required Guid Id { get; set; }
    public required Guid SpeakerId { get; set; }
    public required string Title { get; set; }
    public required string Place { get; set; }
    public required DateOnly EventDate { get; set; }
}
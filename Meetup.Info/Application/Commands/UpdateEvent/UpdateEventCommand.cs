using MediatR;
using Meetup.Info.Application.DTOs;

namespace Meetup.Info.Application.Commands.UpdateEvent;

public class UpdateEventCommand : IRequest<EventDetailsDTO>
{
    public required Guid Id { get; set; }
    public required Guid SpeakerId { get; set; }
    public required string Place { get; set; }
    public required string Title { get; set; }
    public required DateOnly EventDate { get; set; }
}
using MediatR;
using Meetup.Info.Application.DTOs;
using Meetup.Info.Models;

namespace Meetup.Info.Application.Commands.CreateEvent;

public class CreateEventCommand : IRequest<EventDetailsDTO>
{
    public required string Title { get; set; }
    public required DateOnly EventDate { get; set; }
    public required Guid SpeakerId { get; set; } 
    public required string Place { get; set; }
}
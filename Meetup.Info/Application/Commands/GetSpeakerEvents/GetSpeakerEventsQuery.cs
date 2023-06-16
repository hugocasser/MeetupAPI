using MediatR;
using Meetup.Info.Application.DTOs;
using Meetup.Info.Models;

namespace Meetup.Info.Application.Commands.GetSpeakerEvents;

public class GetSpeakerEventsQuery : IRequest<IEnumerable<EventDetailsDTO>>, IRequest<EventDetailsDTO>
{
    public required Guid Id { get; set; }
}
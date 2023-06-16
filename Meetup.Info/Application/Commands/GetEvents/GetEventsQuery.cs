using MediatR;
using Meetup.Info.Application.DTOs;

namespace Meetup.Info.Application.Commands.GetEvents;

public class GetEventsQuery : IRequest<IEnumerable<EventDetailsDTO>>
{
    
}
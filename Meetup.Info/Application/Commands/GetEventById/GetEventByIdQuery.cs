using MediatR;
using Meetup.Info.Application.DTOs;

namespace Meetup.Info.Application.Commands.GetEventById;

public class GetEventByIdQuery : IRequest<EventDetailsDTO>
{
    public required Guid Id { get; set; }
}
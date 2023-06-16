using MediatR;
using Meetup.Info.Application.DTOs;

namespace Meetup.Info.Application.Commands.GetEventByTitleAndDate;

public class GetEventByTitleAndDateQuery: IRequest<EventDetailsDTO>
{
    public required string Title { get; set; }
    public required DateOnly EventDate { get; set; }
}
using AutoMapper;
using MediatR;
using Meetup.Info.Application.DTOs;
using Meetup.Info.Application.Interfaces;

namespace Meetup.Info.Application.Commands.GetEvents;

public class GetEventsQueryHandler : IRequestHandler<GetEventsQuery, IEnumerable<EventDetailsDTO>>
{
    private readonly IMapper _mapper;
    private readonly IEventRepository _eventRepository;

    public GetEventsQueryHandler(IMapper mapper, IEventRepository eventRepository)
    {
        _mapper = mapper;
        _eventRepository = eventRepository;
    }

    public async Task<IEnumerable<EventDetailsDTO>> Handle(GetEventsQuery request, CancellationToken cancellationToken)
    {
        var events = await _eventRepository.GetAllEventsAsync();
        return _mapper.Map<IEnumerable<EventDetailsDTO>>(events);
    }
}
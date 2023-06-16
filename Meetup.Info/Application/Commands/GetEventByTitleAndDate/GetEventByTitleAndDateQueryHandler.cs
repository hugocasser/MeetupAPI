using AutoMapper;
using MediatR;
using Meetup.Info.Application.Commands.GetEventById;
using Meetup.Info.Application.Commons.Exceptions;
using Meetup.Info.Application.DTOs;
using Meetup.Info.Application.Interfaces;

namespace Meetup.Info.Application.Commands.GetEventByTitleAndDate;

public class GetEventByTitleAndDateQueryHandler : IRequestHandler<GetEventByTitleAndDateQuery, EventDetailsDTO>
{
    private readonly IEventRepository _eventRepository;
    private readonly IMapper _mapper;

    public GetEventByTitleAndDateQueryHandler(IEventRepository eventRepository, IMapper mapper)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
    }

    public async Task<EventDetailsDTO> Handle(GetEventByTitleAndDateQuery request, CancellationToken cancellationToken)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));
        var events = await _eventRepository.GetEventByTitleAndDateAsync(request.Title, request.EventDate);
        if(events is null)
            throw new NotFoundException(nameof(request), nameof(request.Title));

        return _mapper.Map<EventDetailsDTO>(events);
    }
}
using AutoMapper;
using MediatR;
using Meetup.Info.Application.Commons.Exceptions;
using Meetup.Info.Application.DTOs;
using Meetup.Info.Application.Interfaces;

namespace Meetup.Info.Application.Commands.GetEventById;

public class GetEventByIdQueryHandler : IRequestHandler<GetEventByIdQuery, EventDetailsDTO>
{
    private readonly IEventRepository _eventRepository;
    private readonly IMapper _mapper;

    public GetEventByIdQueryHandler(IEventRepository eventRepository, IMapper mapper)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
    }

    public async Task<EventDetailsDTO> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
    {
        var events = await _eventRepository.GetEventByIdAsync(request.Id);
        if(events is null)
            throw new NotFoundException(nameof(request), nameof(request.Id));

        return _mapper.Map<EventDetailsDTO>(events);
    }
}
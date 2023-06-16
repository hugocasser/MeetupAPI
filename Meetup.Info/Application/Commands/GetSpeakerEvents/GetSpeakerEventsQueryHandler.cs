using AutoMapper;
using MediatR;
using Meetup.Info.Application.Commons.Exceptions;
using Meetup.Info.Application.DTOs;
using Meetup.Info.Application.Interfaces;

namespace Meetup.Info.Application.Commands.GetSpeakerEvents;

public class GetSpeakerEventsQueryHandler : IRequestHandler<GetSpeakerEventsQuery, IEnumerable<EventDetailsDTO>>
{
    private readonly IEventRepository _eventRepository;
    private readonly ISpeakerRepository _speakerRepository;
    private readonly IMapper _mapper;
    
    public async Task<IEnumerable<EventDetailsDTO>> Handle(GetSpeakerEventsQuery request, CancellationToken cancellationToken)
    {
        var speaker = _speakerRepository.GetSpeakerByIdAsync(request.Id);
        if (speaker is null)
            throw new NotFoundException(nameof(request), nameof(request.Id));
        var events = await _eventRepository.GetAllSpeakersEventsAsync(request.Id);
        return _mapper.Map<IEnumerable<EventDetailsDTO>>(events);
    }
}
using AutoMapper;
using MediatR;
using Meetup.Info.Application.Commons.Exceptions;
using Meetup.Info.Application.DTOs;
using Meetup.Info.Application.Interfaces;
using Meetup.Info.Models;

namespace Meetup.Info.Application.Commands.CreateEvent;

public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, EventDetailsDTO>
{
    private readonly IEventRepository _eventRepository;
    private readonly ISpeakerRepository _speakerRepository;
    private readonly IMapper _mapper;

    public CreateEventCommandHandler(IEventRepository eventRepository, IMapper mapper, ISpeakerRepository speakerRepository)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
        _speakerRepository = speakerRepository;
    }

    public async Task<EventDetailsDTO> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        if (await _eventRepository.GetEventByTitleAndDateAsync(request.Title, request.EventDate) != null)
            throw new EventRecentlyAddedException(nameof(request), request.EventDate, nameof(request));
        var events = new EventInformation()
        {
            Id = Guid.NewGuid(),
            SpeakerId = request.SpeakerId,
            Title = request.Title,
            Place = request.Place,
            EventDate = request.EventDate,
        };

        await _eventRepository.CreateEventAsync(request.SpeakerId, events);
        await _speakerRepository.AddEventToSpeakerAsync(request.SpeakerId, events);
        await _eventRepository.SaveChangesAsync();
        return _mapper.Map<EventDetailsDTO>(events);
    }
}
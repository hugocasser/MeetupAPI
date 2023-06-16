using AutoMapper;
using MediatR;
using Meetup.Info.Application.Commons.Exceptions;
using Meetup.Info.Application.DTOs;
using Meetup.Info.Application.Interfaces;

namespace Meetup.Info.Application.Commands.UpdateEvent;

public class UpdateEventCommandHandler :IRequestHandler<UpdateEventCommand, EventDetailsDTO>
{
    private readonly IEventRepository _eventRepository;
    private readonly IMapper _mapper;
    public async Task<EventDetailsDTO> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
    {
        var eventUpdate = await _eventRepository.GetEventByIdAsync(request.Id);
        if (eventUpdate is null)
            throw new NotFoundException(nameof(request), nameof(request.Id));
        await _eventRepository.UpdateEventAsync(eventUpdate);
        return _mapper.Map<EventDetailsDTO>(eventUpdate);
    }
}
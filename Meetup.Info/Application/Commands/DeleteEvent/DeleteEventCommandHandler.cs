using MediatR;
using Meetup.Info.Application.Interfaces;
using Meetup.Info.Application.Commons.Exceptions;
namespace Meetup.Info.Application.Commands.DeleteEvent;

public class DeleteEventCommandHandler: IRequestHandler<DeleteEventCommand>
{
    private readonly IEventRepository _eventRepository;

    public DeleteEventCommandHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task Handle(DeleteEventCommand request, CancellationToken cancellationToken)
    {
        if(await _eventRepository.GetEventByIdAsync(request.Id) is null)
            throw new NotFoundException(nameof(request),nameof(request.Id));

        await _eventRepository.DeleteEventAsync(request.Id);
        await _eventRepository.SaveChangesAsync();
    }
}
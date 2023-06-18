using MediatR;
using Meetup.SpeakerService.Application.Interfaces;
using Meetup.SpeakerService.Models;
using Meetup.SpeakerService.Application.Common.CustomExceptions;

namespace Meetup.SpeakerService.Application.Commands.DeleteSpeaker;

public class DeleteSpeakerCommandHandler : IRequestHandler<DeleteSpeakerCommand>
{
    private readonly ISpeakerRepository _speakerRepository;

    public DeleteSpeakerCommandHandler(ISpeakerRepository speakerRepository)
    {
        _speakerRepository = speakerRepository;
    }
    public async Task Handle(DeleteSpeakerCommand request, CancellationToken cancellationToken)
    {
        if (!await _speakerRepository.SpeakerExistAsync(request.Id))
            throw new NotFoundException<Speaker>(nameof(request.Id));
        
        await _speakerRepository.DeleteSpeakerAsync(request.Id);
        await _speakerRepository.SaveChangesAsync();
    }
}
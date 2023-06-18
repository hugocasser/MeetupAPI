using MediatR;
using Meetup.SpeakerService.Application.Interfaces;
using Meetup.SpeakerService.Models;

namespace Meetup.SpeakerService.Application.Commands.CreateSpeaker;

public class CreateSpeakerCommandHandler : IRequestHandler<CreateSpeakerCommand>
{
    private readonly ISpeakerRepository _speakerRepository;

    public CreateSpeakerCommandHandler(ISpeakerRepository speakerRepository)
    {
        _speakerRepository = speakerRepository;
    }
    public async Task Handle(CreateSpeakerCommand request, CancellationToken cancellationToken)
    {
        var speaker = new Speaker
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Presentation = request.Presentation
        };

        await _speakerRepository.CreateSpeakerAsync(speaker);
        await _speakerRepository.SaveChangesAsync();
    }
}
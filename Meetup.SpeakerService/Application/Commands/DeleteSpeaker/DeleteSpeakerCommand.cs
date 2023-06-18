using MediatR;

namespace Meetup.SpeakerService.Application.Commands.DeleteSpeaker;

public class DeleteSpeakerCommand : IRequest
{
    public required Guid Id { get; set; }
}
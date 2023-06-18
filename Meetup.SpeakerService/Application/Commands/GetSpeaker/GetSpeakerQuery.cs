using MediatR;
using Meetup.SpeakerService.Application.DTOs;

namespace Meetup.SpeakerService.Application.Commands.GetSpeaker;

public class GetSpeakerQuery : IRequest<SpeakerDetailsDTO>
{
    public required Guid Id { get; set; }
}
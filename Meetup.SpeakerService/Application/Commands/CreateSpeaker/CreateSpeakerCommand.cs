using MediatR;
using Meetup.SpeakerService.Models;

namespace Meetup.SpeakerService.Application.Commands.CreateSpeaker;

public class CreateSpeakerCommand : IRequest<Speaker>, IRequest
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Presentation { get; set; }
}
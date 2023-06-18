using MediatR;
using Meetup.SpeakerService.Application.DTOs;
using Meetup.SpeakerService.Models;

namespace Meetup.SpeakerService.Application.Commands.UpdateSpeaker;

public class UpdateSpeakerCommand : IRequest<SpeakerDetailsDTO>
{
    public required Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Presentation { get; set; }
}
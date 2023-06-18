using MediatR;
using Meetup.SpeakerService.Application.DTOs;

namespace Meetup.SpeakerService.Application.Commands.GetSpeakersQuery;

public class GetSpeakersQuery  : IRequest<IEnumerable<SpeakerDetailsDTO>>
{
    
}
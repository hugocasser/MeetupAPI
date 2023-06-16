using MediatR;
using Meetup.Info.Application.DTOs;

namespace Meetup.Info.Application.Commands.GetSpeakers;

public class GetSpeakersQuery : IRequest<IEnumerable<SpeakerDetailsDTO>>
{
    
}
using AutoMapper;
using MediatR;
using Meetup.Info.Application.DTOs;
using Meetup.Info.Application.Interfaces;

namespace Meetup.Info.Application.Commands.GetSpeakers;

public class GetSpeakersQueryHandler : IRequestHandler<GetSpeakersQuery, IEnumerable<SpeakerDetailsDTO>>
{
    private readonly ISpeakerRepository _speakerRepository;
    private readonly IMapper _mapper;
    public async Task<IEnumerable<SpeakerDetailsDTO>> Handle(GetSpeakersQuery request, CancellationToken cancellationToken)
    {
        var speakers = await _speakerRepository.GetAllSpeakersAsync();
        return _mapper.Map<IEnumerable<SpeakerDetailsDTO>>(speakers);
    }
}
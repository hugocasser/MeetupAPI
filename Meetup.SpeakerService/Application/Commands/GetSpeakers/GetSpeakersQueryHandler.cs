using AutoMapper;
using MediatR;
using Meetup.SpeakerService.Application.DTOs;
using Meetup.SpeakerService.Application.Interfaces;
using Meetup.SpeakerService.Models;

namespace Meetup.SpeakerService.Application.Commands.GetSpeakersQuery;

public class GetSpeakersQueryHandler : IRequestHandler<GetSpeakersQuery, IEnumerable<SpeakerDetailsDTO>>
{
    private readonly ISpeakerRepository _speakerRepository;
    private readonly IMapper _mapper;

    public GetSpeakersQueryHandler(ISpeakerRepository speakerRepository, IMapper mapper)
    {
        _speakerRepository = speakerRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<SpeakerDetailsDTO>> Handle(GetSpeakersQuery request, CancellationToken cancellationToken)
    {
        var speakers = await _speakerRepository.GetAllSpeakersAsync();
        return _mapper.Map<IEnumerable<SpeakerDetailsDTO>>(speakers);
    }
}
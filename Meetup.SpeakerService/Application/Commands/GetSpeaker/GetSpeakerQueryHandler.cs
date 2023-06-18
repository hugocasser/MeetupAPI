using AutoMapper;
using MediatR;
using Meetup.SpeakerService.Application.Common.CustomExceptions;
using Meetup.SpeakerService.Application.DTOs;
using Meetup.SpeakerService.Application.Interfaces;
using Meetup.SpeakerService.Models;

namespace Meetup.SpeakerService.Application.Commands.GetSpeaker;

public class GetSpeakerQueryHandler : IRequestHandler<GetSpeakerQuery, SpeakerDetailsDTO>
{
    private readonly ISpeakerRepository _speakerRepository;
    private readonly IMapper _mapper;

    public GetSpeakerQueryHandler(ISpeakerRepository speakerRepository, IMapper mapper)
    {
        _speakerRepository = speakerRepository;
        _mapper = mapper;
    }

    public async Task<SpeakerDetailsDTO> Handle(GetSpeakerQuery request, CancellationToken cancellationToken)
    {
        if (!await _speakerRepository.SpeakerExistAsync(request.Id))
            throw new NotFoundException<Speaker>(nameof(request.Id));
        
        var speakers = await _speakerRepository.GetSpeakerByIdAsync(request.Id);
        return _mapper.Map<SpeakerDetailsDTO>(speakers);
    }
}
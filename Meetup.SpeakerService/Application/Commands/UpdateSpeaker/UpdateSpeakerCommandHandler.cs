using AutoMapper;
using MediatR;
using Meetup.SpeakerService.Application.Common.CustomExceptions;
using Meetup.SpeakerService.Application.DTOs;
using Meetup.SpeakerService.Application.Interfaces;
using Meetup.SpeakerService.Models;

namespace Meetup.SpeakerService.Application.Commands.UpdateSpeaker;

public class UpdateSpeakerCommandHandler : IRequestHandler<UpdateSpeakerCommand, SpeakerDetailsDTO>
{
    private readonly ISpeakerRepository _speakerRepository;
    private readonly IMapper _mapper;

    public UpdateSpeakerCommandHandler(ISpeakerRepository speakerRepository, IMapper mapper)
    {
        _speakerRepository = speakerRepository;
        _mapper = mapper;
    }
    public async Task<SpeakerDetailsDTO> Handle(UpdateSpeakerCommand request, CancellationToken cancellationToken)
    {
        if (!await _speakerRepository.SpeakerExistAsync(request.Id)) 
            throw new NotFoundException<Speaker>(nameof(request));
        var speaker = new Speaker
        {
            Id = request.Id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Presentation = request.Presentation
        };
        await _speakerRepository.UpdateSpeakerAsync(speaker);
        await _speakerRepository.SaveChangesAsync();
        return _mapper.Map<SpeakerDetailsDTO>(speaker);
    }
}
using AutoMapper;
using Meetup.SpeakerService.Application.Commands.DeleteSpeaker;
using Meetup.SpeakerService.Application.Common.Mapping;

namespace Meetup.SpeakerService.Application.DTOs;

public class SpeakerDeleteDTO : IMapWith<DeleteSpeakerCommand>
{
    
    public required Guid Id { get; set; }
    public required string Event { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<DeleteSpeakerCommand, SpeakerDeleteDTO>()
            .ForMember(
                dto => dto.Id,
                expression => expression.MapFrom(speakerDto =>speakerDto.Id));
    }
}
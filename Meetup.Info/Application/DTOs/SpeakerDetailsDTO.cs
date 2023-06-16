using AutoMapper;
using Meetup.Info.Application.Commons.Mappings;
using Meetup.Info.Models;

namespace Meetup.Info.Application.DTOs;

public class SpeakerDetailsDTO :IMapWith<Speaker>
{
    public required Guid Id { get; set; }
    public required string FirstName { get; set; }
    
    public required string LastName { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Speaker, SpeakerDetailsDTO>()
            .ForMember(
                dto => dto.Id,
                expression => expression.MapFrom(speaker => speaker.Id))
            .ForMember(
                dto => dto.FirstName,
                expression => expression.MapFrom(speaker => speaker.FirstName))
            .ForMember(
                dto => dto.LastName,
                expression => expression.MapFrom(speaker => speaker.LastName));
    }
}
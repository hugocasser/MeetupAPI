using AutoMapper;
using Meetup.Info.Application.Commons.Mappings;
using Meetup.Info.Models;

namespace Meetup.Info.Application.DTOs;

public class SpeakerPublishedDTO : IMapWith<Speaker>
{
    public required Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Event { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SpeakerPublishedDTO, Speaker>()
            .ForMember(
                dto => dto.ExternalId,
                expression => expression.MapFrom(authorsDto => authorsDto.Id))
            .ForMember(
                dto => dto.FirstName,
                expression => expression.MapFrom(authorsDto => authorsDto.FirstName))
            .ForMember(
                dto => dto.LastName,
                expression => expression.MapFrom(authorsDto => authorsDto.LastName));;
    }
}
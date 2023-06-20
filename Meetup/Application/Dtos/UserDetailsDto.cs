using AutoMapper;
using Meetup.Application.Common.Mappings;
using Meetup.Models;

namespace Meetup.Application.Dtos;

public class UserDetailsDto : IMapWith<User>
{
    public required Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<User, UserDetailsDto>()
            .ForMember(
                dto => dto.Id,
                expression => expression.MapFrom(user => user.Id))
            .ForMember(
                dto => dto.FirstName,
                expression => expression.MapFrom(user => user.FirstName))
            .ForMember(
                dto => dto.LastName,
                expression => expression.MapFrom(user => user.LastName));
    }
}
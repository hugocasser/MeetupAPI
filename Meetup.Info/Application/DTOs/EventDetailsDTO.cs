using AutoMapper;
using Meetup.Info.Application.Commons.Mappings;
using Meetup.Info.Models;
using Meetup.Info.Persistence;

namespace Meetup.Info.Application.DTOs;

public class EventDetailsDTO : IMapWith<EventInformation>
{
    public required Guid Id { get; set; }
    public required string Title { get; set; }
    public required DateTime EventDate { get; set; }
    public required Guid SpeakerId { get; set; } 
    public required string Place { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EventInformation, EventDetailsDTO>()
            .ForMember(
                dto => dto.Id,
                expression => expression.MapFrom(events => events.Id))
            .ForMember(
                dto => dto.Title,
                expression => expression.MapFrom(events => events.Title))
            .ForMember(
                dto => dto.EventDate,
                expression => expression.MapFrom(events => events.EventDate))
            .ForMember(
                dto => dto.Place,
                expression => expression.MapFrom(events => events.Place))
            .ForMember(
                dto => dto.SpeakerId,
                expression => expression.MapFrom(events => events.SpeakerId));
    }
}
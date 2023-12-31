﻿using AutoMapper;

namespace Meetup.SpeakerService.Application.DTOs;

public class SpeakerPublishDTO
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Event { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SpeakerDetailsDTO, SpeakerPublishDTO>()
            .ForMember(
                dto => dto.Id,
                expression => expression.MapFrom(speakerDto => speakerDto.Id))
            .ForMember(
                dto => dto.Name,
                expression => expression.MapFrom(speakerDto => $"{speakerDto.FirstName} {speakerDto.LastName}"));
    }
}
using System.Reflection;
using Meetup.Info.Application.Commons.Mappings;
using Meetup.Info.Models;

namespace Meetup.Info.Application.DTOs;

public class EventCreateDTO : IMapWith<EventInfo>
{
    public required Guid Id { get; set; }
    public required Guid  SpeakerId { get; set; }
    public required string Title { get; set; }
    public required string Place { get; set; }
    public required DateOnly EventDate { get; set; }
}
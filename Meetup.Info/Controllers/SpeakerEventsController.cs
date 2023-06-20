using AutoMapper;
using MediatR;
using Meetup.Info.Application.Commands.CreateEvent;
using Meetup.Info.Application.Commands.DeleteEvent;
using Meetup.Info.Application.Commands.GetSpeakerEvents;
using Meetup.Info.Application.Commands.UpdateEvent;
using Meetup.Info.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Meetup.Info.Controllers;

[Route("api/speakers/{speakerId:guid}/events")]
[SwaggerTag("Speaker and event relation controller")]
[ApiController]
public class SpeakerEventController : ControllerBase
{
    private readonly IMapper _mapper;
    private IMediator? _mediator;
    private IMediator? Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    
    public SpeakerEventController(IMapper mapper)
    {
        _mapper = mapper;
    }
    
    [SwaggerOperation(Summary = "Get all events from speaker by ID")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EventDetailsDTO>>> GetSpeakerEvents(Guid speakerId)
    {
        if (Mediator is null)
            return BadRequest("Internal server error");
        
        return Ok( await Mediator.Send(new GetSpeakerEventsQuery
        {
            Id = speakerId
        }));
    }
    
    
    [SwaggerOperation(Summary = "Create event from speaker")]
    [SwaggerResponse(200, "Returns event Details Model")]
    [HttpPost]
    public async Task<ActionResult<EventDetailsDTO>> CreateEvent(Guid speakerId, [FromBody] EventCreateDTO eventCreateDTO)
    {
        if (Mediator is null)
            return BadRequest("Internal server error");

        var events = await Mediator.Send(new CreateEventCommand
        {
            Title = eventCreateDTO.Title,
            EventDate = eventCreateDTO.EventDate,
            SpeakerId = speakerId,
            Place = eventCreateDTO.Place
        });

        return Ok(_mapper.Map<EventDetailsDTO>(events));
    }

    [SwaggerOperation(Summary = "Update Event by Speaker Id and Event Id")]
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<EventDetailsDTO>> UpdateEvent(Guid speakerId, [FromBody] EventUpdateDTO eventUpdateDTO)
    {
        if (Mediator is null)
            return BadRequest("Internal server error");

        var updateEventCommand = new UpdateEventCommand
        {
            Id = eventUpdateDTO.Id,
            SpeakerId = speakerId,
            Title = eventUpdateDTO.Title,
            Place = eventUpdateDTO.Place,
            EventDate = eventUpdateDTO.EventDate
        };
        
        var updatedEvent = await Mediator.Send(updateEventCommand);
        return Ok(updatedEvent);
    }
    
    [SwaggerOperation(Summary = "Delete event by Id")]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<EventDetailsDTO>> DeleteEvent(Guid id)
    {
        if (Mediator is null)
            return BadRequest("Internal server error");

        var deleteEventCommand = new DeleteEventCommand()
        {
            Id = id
        };
        await Mediator.Send(deleteEventCommand);
        return Ok(deleteEventCommand);
    }
}
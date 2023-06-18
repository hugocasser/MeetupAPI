using AutoMapper;
using MediatR;
using Meetup.SpeakerService.Application.Commands.CreateSpeaker;
using Meetup.SpeakerService.Application.Commands.DeleteSpeaker;
using Meetup.SpeakerService.Application.Commands.GetSpeaker;
using Meetup.SpeakerService.Application.Commands.GetSpeakersQuery;
using Meetup.SpeakerService.Application.Commands.UpdateSpeaker;
using Meetup.SpeakerService.Application.DTOs;
using Meetup.SpeakerService.Services.RabbitMQ;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Meetup.SpeakerService.Controllers;

[SwaggerTag("Speaker service endpoints")]
[Route("api/[controller]/")]
[ApiController]

public class SpeakersController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMessageBusClient _messageBusClient;
    private IMediator? _mediator;
    private IMediator? Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    
    public SpeakersController(IMapper mapper, IMessageBusClient messageBusClient, IMediator? mediator)
    {
        _mediator = mediator;
        _mapper = mapper;
        _messageBusClient = messageBusClient;
    }
    
    [SwaggerOperation(Summary = "Get all speakers")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SpeakerDetailsDTO>>> GetAllSpeakers()
    {
        if (Mediator is null)
            return BadRequest("Internal server error");

        var speakers = await Mediator.Send(new GetSpeakersQuery());
        return Ok(speakers);
    }
    
    [SwaggerOperation(Summary = "Get detail information about speaker")]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<SpeakerDetailsDTO>> GetSpeakerDetails(Guid id)
    {
        if (Mediator is null)
            return BadRequest("Internal server error");

        var speaker = await Mediator.Send(new GetSpeakerQuery()
        {
            Id = id
        });
        
        return Ok(speaker);
    }
    
    [SwaggerOperation(Summary = "Create speaker")]
    [HttpPost]
    public async Task<ActionResult<SpeakerDetailsDTO>> CreateAuthor([FromBody] CreateSpeakerCommand createSpeakerCommand)
    {
        if (Mediator is null)
            return BadRequest("Internal server error");

        var speaker = await Mediator.Send(createSpeakerCommand);
        var speakerDTO = _mapper.Map<SpeakerDetailsDTO>(speaker);

        var platformPublishDTO = _mapper.Map<SpeakerPublishDTO>(speakerDTO);
        platformPublishDTO.Event = "Speaker_Published";
        _messageBusClient.PublishSpeaker(platformPublishDTO);
        
        return Ok(speakerDTO);
    }
    
    [SwaggerOperation(Summary = "Update existing speaker")]
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<SpeakerDetailsDTO>> UpdateSpeaker(Guid id, [FromBody] SpeakerUpdateDTO speakerUpdateDTO)
    {
        if (Mediator is null)
            return BadRequest("Internal server error");

        var updateAuthorCommand = new UpdateSpeakerCommand
        {
            Id = id,
            FirstName = speakerUpdateDTO.FirstName,
            LastName = speakerUpdateDTO.LastName,
            Presentation = speakerUpdateDTO.Presentation
        };
        
        var updatedSpeaker = await Mediator.Send(updateAuthorCommand);
        var platformPublishDto = _mapper.Map<SpeakerPublishDTO>(updatedSpeaker);
        platformPublishDto.Event = "Speaker_Updated";
        _messageBusClient.PublishUpdateSpeaker(platformPublishDto);

        return Ok(updatedSpeaker);
    }
    
    [SwaggerOperation(Summary = "Delete speaker by Id")]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<SpeakerDetailsDTO>> DeleteAuthor(Guid id)
    {
        if (Mediator is null)
            return BadRequest("Internal server error");

        var deleteSpeakerCommand = new DeleteSpeakerCommand()
        {
            Id = id
        };
        
        await Mediator.Send(deleteSpeakerCommand);
        var platformPublishDto = _mapper.Map<SpeakerDeleteDTO>(deleteSpeakerCommand);
        platformPublishDto.Event = "Speakers_Deleted";
        _messageBusClient.PublishDeleteSpeaker(platformPublishDto);

        return Ok(deleteSpeakerCommand);
    }
}
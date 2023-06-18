using System.Text.Json;
using AutoMapper;
using Meetup.Info.Application.DTOs;
using Meetup.Info.Application.Interfaces;
using Meetup.Info.Models;

namespace Meetup.Info.Services.RabbitMQProcessing;

public class EventProcessor : IEventProcessor
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<EventProcessor> _logger;
    private readonly IMapper _mapper;

    public EventProcessor(IServiceScopeFactory serviceScopeFactory, IMapper mapper, ILogger<EventProcessor> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _mapper = mapper;
        _logger = logger;
    }

    public void ProcessEvent(string message)
    {
        _logger.LogInformation("--> Determining Event");
        var eventType = DetermineEvent(message);
        switch (eventType)
        {
            case EventType.SpeakerPublished:
                AddSpeaker(message);
                break;
            case EventType.SpeakerDeleted:
                DeleteSpeaker(message);
                break;
            case EventType.SpeakerUpdated:
                UpdateSpeaker(message);
                break;
        }
    }

    private async void AddSpeaker(string speakerPublishedMessage)
    {
        _logger.LogInformation("--> Speaker started added process!");

        using var scope = _serviceScopeFactory.CreateScope();
        var speakerRepository = scope.ServiceProvider.GetRequiredService<ISpeakerRepository>();

        var speakerPublishedDTO = JsonSerializer.Deserialize<SpeakerPublishedDTO>(speakerPublishedMessage);
        var speaker = _mapper.Map<Speaker>(speakerPublishedDTO);
        if (await speakerRepository.ExternalSpeakerExistAsync(speakerPublishedDTO.Id))
            return;

        speaker.Id = Guid.NewGuid();
        await speakerRepository.CreateSpeakerAsync(speaker);
        await speakerRepository.SaveChangesAsync();

        _logger.LogInformation("--> Speaker added!");
    }

    private async void UpdateSpeaker(string speakerPublishedMessage)
    {
        _logger.LogInformation("--> Speaker started update process!");

        using var scope = _serviceScopeFactory.CreateScope();
        var speakerRepository = scope.ServiceProvider.GetRequiredService<ISpeakerRepository>();

        var speakerDTO = JsonSerializer.Deserialize<SpeakerPublishedDTO>(speakerPublishedMessage);
        var speaker = _mapper.Map<Speaker>(speakerDTO);
        if (!await speakerRepository.ExternalSpeakerExistAsync(speaker.Id))
            return;

        await speakerRepository.UpdateSpeakerAsync(speaker);
        await speakerRepository.SaveChangesAsync();

        _logger.LogInformation("--> Speaker updated!");
    }

    private async void DeleteSpeaker(string speakerPublishedMessage)
    {
        _logger.LogInformation("--> Speaker started delete process!");

        using var scope = _serviceScopeFactory.CreateScope();
        var speakerRepository = scope.ServiceProvider.GetRequiredService<ISpeakerRepository>();

        var speakerDeletedId = JsonSerializer.Deserialize<SpeakerDeleteDTO>(speakerPublishedMessage);
        if (!await speakerRepository.ExternalSpeakerExistAsync(speakerDeletedId.Id))
            return;

        await speakerRepository.DeleteSpeakerByExternalIdAsync(speakerDeletedId.Id);
        await speakerRepository.SaveChangesAsync();

        _logger.LogInformation("--> Author deleted!");
    }

    private EventType DetermineEvent(string notificationMessage)
    {
        var eventType = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);
        return eventType?.Event switch
        {
            "Speaker_Published" => EventType.SpeakerPublished,
            "Speaker_Updated" => EventType.SpeakerUpdated,
            "Speaker_Deleted" => EventType.SpeakerDeleted,
            _ => EventType.Undetermined
        };
    }
}
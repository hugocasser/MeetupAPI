using System.Collections.Immutable;
using System.Text;
using System.Text.Json;
using Meetup.Application.Dtos;
using Meetup.Models;

namespace Meetup.Services;

public class EventDataClient: IEventDataClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ILogger<EventDataClient> _logger;
    
    public EventDataClient(HttpClient httpClient, IConfiguration configuration, ILogger<EventDataClient> logger)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _logger = logger;
    }
    
    public async Task<IEnumerable<EventInformation>> GetAllEvents()
    {
        var request = await _httpClient.GetAsync(_configuration.GetServiceUri("modsen-info") + "api/Events");
        if (request.IsSuccessStatusCode)
        {
            _logger.LogInformation("Sync Get Events to Events service was successful");
            var events = await JsonSerializer.DeserializeAsync<IEnumerable<EventInformation>>(await request.Content.ReadAsStreamAsync(), new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            if (events is not null)
                return events;
            _logger.LogError("Failed to parse data from events service");
            throw new NullReferenceException(nameof(EventInformation));
        }
        
        _logger.LogError("Sync Get request to events service was not successful");
        return ImmutableArray<EventInformation>.Empty;
    }

    public async Task<EventInformation> GetEventById(Guid id)
    {
        var request = await _httpClient.GetAsync(_configuration.GetServiceUri("modsen-info") + "api/Events/" + id);
        if (request.IsSuccessStatusCode)
        {
            _logger.LogInformation("Sync Get events to events service was successful");
            var events = await JsonSerializer.DeserializeAsync<EventInformation>(await request.Content.ReadAsStreamAsync(), new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            if (events is not null)
                return events;
            
            _logger.LogError("Failed to parse data from events service");
            throw new NullReferenceException(nameof(EventInformation));
        }
        
        _logger.LogError("Sync Get request to events service was not successful");
        return new EventInformation
        {
            Id = default,
            Title = null,
            EventDate = default,
            Place = null
        };
    }

    public async Task<CreateEventDto> CreateEvent(CreateEventDto eventDto)
    {
        using var jsonContent = new StringContent(
            JsonSerializer.Serialize(eventDto),
            Encoding.UTF8,
            "application/json");
        var request = await _httpClient.PostAsync(_configuration.GetServiceUri("modsen-info") + $"api/speakers//{eventDto.SpeakerId}/events", jsonContent);
        if (request.IsSuccessStatusCode)
            _logger.LogInformation("Sync Post Events to Events service was successful");
        else
            _logger.LogError("Sync Post request to Events service was not successful");
        
        return eventDto;
    }

    public async Task<UpdateEventDto> UpdateEvent(UpdateEventDto eventDto)
    {
        using var jsonContent = new StringContent(
            JsonSerializer.Serialize(eventDto),
            Encoding.UTF8,
            "application/json");
        var request = await _httpClient.PutAsync(_configuration.GetServiceUri("modsen-info") + $"api/speakers//{eventDto.SpeakerId}/events", jsonContent);
        if (request.IsSuccessStatusCode)
            _logger.LogInformation("Sync Put Events to Events service was successful");
        else
            _logger.LogError("Sync Put request to Events service was not successful");
        
        return eventDto;
    }

    public async Task DeleteEvent(DeleteEventDto eventDto)
    {
        var request = await _httpClient.DeleteAsync(_configuration.GetServiceUri("modsen-info") +
                                                    $"api/speakers//{eventDto.SpeakerId}/events" + eventDto.Id);
    }
}
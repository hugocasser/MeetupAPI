using System.Collections.Immutable;
using System.Text;
using System.Text.Json;
using Meetup.Application.Dtos;
using Meetup.Models;

namespace Meetup.Services;

public class SpeakerDataClient : ISpeakerDataClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ILogger<SpeakerDataClient> _logger;
    
    public SpeakerDataClient(HttpClient httpClient, IConfiguration configuration, ILogger<SpeakerDataClient> logger)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<IEnumerable<Speaker>> GetAllSpeakers()
    {
        var request = await _httpClient.GetAsync(_configuration.GetServiceUri("modsen-speakers") + "api/SpeakersController");
        if (request.IsSuccessStatusCode)
        {
            _logger.LogInformation("Sync Get request to speaker service was successful");
            var speakers =
                await JsonSerializer.DeserializeAsync<IEnumerable<Speaker>>(await request.Content.ReadAsStreamAsync(), new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
            
            if (speakers is not null) 
                return speakers;
            
            _logger.LogError("Failed to parse data from speaker service");
            throw new NullReferenceException(nameof(speakers));
        }
        
        _logger.LogError("Sync Get request to speaker service was not successful");
        return ImmutableArray<Speaker>.Empty;
    }

    public async Task<CreateSpeakerDto> CreateSpeaker(CreateSpeakerDto createSpeakerDto)
    {
        using var jsonContent = new StringContent(
            JsonSerializer.Serialize(createSpeakerDto), 
            Encoding.UTF8,
            "application/json");
        var request = await _httpClient.PostAsync(_configuration.GetServiceUri("modsen-speakers") + "api/SpeakersController", jsonContent);
        if (request.IsSuccessStatusCode)
            _logger.LogInformation("Sync Post speaker to speakers service was successful");
        else
            _logger.LogError("Sync Post request to speakers service was not successful");
        
        return createSpeakerDto;       
    }
}
using Meetup.Info.Models;

namespace Meetup.Info.Application.Interfaces;

public interface ISpeakerRepository
{
    public Task<IEnumerable<Speaker?>> GetAllSpeakersAsync();
    public Task<Speaker?> CreateSpeakerAsync(Speaker? speaker);
    public Task<Speaker?> GetSpeakerByIdAsync(Guid speakerId);
    public Task AddEventToSpeakerAsync(Guid speakerId, EventInformation eventInformation);
    public Task DeleteSpeakerByExternalIdAsync(Guid externalId);
    public Task UpdateSpeakerAsync(Speaker speaker);
    public Task<bool> SpeakerExistAsync(Guid speakerId);
    public Task<bool> ExternalSpeakerExistAsync(Guid speakerId);
    public Task  SaveChangesAsync();
    public Task<Speaker?> GetSpeakerByExternalIdAsync(Guid externalId);
}
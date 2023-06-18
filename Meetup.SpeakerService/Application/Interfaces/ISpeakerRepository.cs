using Meetup.SpeakerService.Models;

namespace Meetup.SpeakerService.Application.Interfaces;

public interface ISpeakerRepository
{
    public Task<IEnumerable<Speaker>> GetAllSpeakersAsync();
    public Task<Speaker?> GetSpeakerByIdAsync(Guid id);
    public Task<bool> SpeakerExistAsync(Guid id);
    public Task CreateSpeakerAsync(Speaker speaker);
    public Task DeleteSpeakerAsync(Guid id);
    public Task UpdateSpeakerAsync(Speaker speaker);
    public Task<bool> SaveChangesAsync();
}
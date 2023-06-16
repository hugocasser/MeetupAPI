using Meetup.Info.Models;

namespace Meetup.Info.Application.Interfaces;

public interface IEventRepository
{
    public Task<IEnumerable<EventInformation>> GetAllEventsAsync();
    public Task<EventInformation?> GetEventByIdAsync(Guid eventId);
    public Task<EventInformation?> GetEventByTitleAndDateAsync(string title, DateOnly date);
    public Task UpdateEventAsync(EventInformation eventInformation);
    public Task DeleteEventAsync(Guid id);
    public Task CreateEventAsync(Guid speakerId, EventInformation eventInformation);
    public Task<IEnumerable<EventInformation>> GetAllSpeakersEventsAsync(Guid speakerId);
    public Task SaveChangesAsync();
}
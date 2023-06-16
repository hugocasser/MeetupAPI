using Meetup.Info.Application.Interfaces;
using Meetup.Info.Models;
using Microsoft.EntityFrameworkCore;

namespace Meetup.Info.Persistence.Repositories;

public class EventsRepository : IEventRepository
{
    private readonly EventInformationDbContext _context;

    public EventsRepository(EventInformationDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<EventInformation>> GetAllEventsAsync()
    {
        return await _context.Events.ToListAsync();
    }

    public async Task<EventInformation?> GetEventByIdAsync(Guid eventId)
    {
        return await _context.Events.FirstOrDefaultAsync(events => events.Id == eventId);
    }

    public async Task<EventInformation?> GetEventByTitleAndDateAsync(string title, DateOnly date)
    {
        return await _context.Events.FirstOrDefaultAsync(events => events.Title == title && events.EventDate == date);
    }

    public async Task UpdateEventAsync(EventInformation eventInformation)
    {
        var updateEvent = await GetEventByIdAsync(eventInformation.Id);
        if (updateEvent is null)
            return;
        updateEvent.EventDate = eventInformation.EventDate;
        updateEvent.Place = eventInformation.Place;
        updateEvent.Title = eventInformation.Title;
    }

    public async Task DeleteEventAsync(Guid id)
    {
        var deleteEvent = await GetEventByIdAsync(id);
        if (deleteEvent is null)
            return;
        _context.Remove(deleteEvent);
    }

    public async Task CreateEventAsync(Guid speakerId, EventInformation eventInformation)
    {
        if (eventInformation is null)
            throw new ArgumentNullException(nameof(eventInformation));

        eventInformation.SpeakerId = speakerId;
        await _context.Events.AddAsync(eventInformation);
    }

    public async Task<IEnumerable<EventInformation>> GetAllSpeakersEventsAsync(Guid speakerId)
    {
        return await _context.Events.Where(events => events.SpeakerId == speakerId).ToListAsync();
    }

    public async Task SaveChangesAsync()
    { 
        await _context.SaveChangesAsync() ;
    }
}
using Meetup.Info.Application.Interfaces;
using Meetup.Info.Models;
using Microsoft.EntityFrameworkCore;

namespace Meetup.Info.Persistence.Repositories;

public class SpeakersRepository : ISpeakerRepository
{
    private readonly EventInformationDbContext _context;

    public SpeakersRepository(EventInformationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Speaker?>> GetAllSpeakersAsync()
    {
        return await _context.Speakers.ToListAsync();
    }

    public async Task<Speaker?> CreateSpeakerAsync(Speaker? speaker)
    {
        if (speaker is null)
            throw new ArgumentNullException(nameof(speaker));

        await _context.Speakers.AddAsync(speaker);
        return speaker;
    }

    public async Task<Speaker?> GetSpeakerByIdAsync(Guid speakerId)
    {
        return await _context.Speakers.FirstOrDefaultAsync(speaker => speaker.Id == speakerId);
    }

    public async Task AddEventToSpeakerAsync(Guid speakerId, EventInformation eventInformation)
    {
        var speaker = await GetSpeakerByIdAsync(speakerId);
        speaker?.Events.Add(eventInformation);
    }

    public async Task DeleteSpeakerByExternalIdAsync(Guid externalId)
    {
        var speaker = await GetSpeakerByExternalIdAsync(externalId);
        if (speaker is null)
            return;
        _context.Speakers.Remove(speaker);
    }

    public async Task UpdateSpeakerAsync(Speaker speaker)
    {
        var speakerUpdate = await GetSpeakerByIdAsync(speaker.Id);
        if (speakerUpdate is null)
            return;
        speakerUpdate.FirstName = speaker.FirstName;
        speakerUpdate.LastName = speaker.LastName;
    }

    public async Task<bool> SpeakerExistAsync(Guid speakerId)
    {
        return await _context.Speakers.AnyAsync(speaker => speaker.Id == speakerId);
    }

    public async Task<bool> ExternalSpeakerExistAsync(Guid externalSpeakerId)
    {
        return await _context.Speakers.AnyAsync(speaker => speaker.ExternalId == externalSpeakerId);
    }
    public async Task<Speaker?> GetSpeakerByExternalIdAsync(Guid externalId)
    {
        return await _context.Speakers.FirstOrDefaultAsync(speaker => speaker.ExternalId == externalId);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }


}
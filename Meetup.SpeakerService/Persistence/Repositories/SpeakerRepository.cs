using Meetup.SpeakerService.Application.Interfaces;
using Meetup.SpeakerService.Models;
using Microsoft.EntityFrameworkCore;

namespace Meetup.SpeakerService.Persistence.Repositories;

public class SpeakerRepository : ISpeakerRepository
{
    private readonly SpeakerDbContext _context;

    public SpeakerRepository(SpeakerDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Speaker>> GetAllSpeakersAsync()
    {
        return await _context.Speakers.ToListAsync();
    }

    public async Task<Speaker?> GetSpeakerByIdAsync(Guid id)
    {
        return await _context.Speakers.FirstOrDefaultAsync(speaker => speaker.Id == id);
    }

    public async Task<bool> SpeakerExistAsync(Guid id)
    {
        return await _context.Speakers.AnyAsync(speaker => speaker.Id == id);
    }

    public async Task CreateSpeakerAsync(Speaker speaker)
    {
        if (speaker is null)
            throw new ArgumentNullException(); 
        await _context.Speakers.AddAsync(speaker);
    }

    public async Task DeleteSpeakerAsync(Guid id)
    {
        var speaker = await GetSpeakerByIdAsync(id);
        if (speaker is null)
            return;
                
        _context.Speakers.Remove(speaker);
    }

    public async Task UpdateSpeakerAsync(Speaker speaker)
    {
        var speakerToUpdate = await GetSpeakerByIdAsync(speaker.Id);
        if( speakerToUpdate is null)
            return;
        speakerToUpdate.FirstName = speaker.FirstName;
        speakerToUpdate.LastName = speaker.LastName;
        speakerToUpdate.Presentation = speaker.Presentation;
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0 ;
    }
}
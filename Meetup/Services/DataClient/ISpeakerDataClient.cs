using Meetup.Application.Dtos;
using Meetup.Models;

namespace Meetup.Services;

public interface ISpeakerDataClient
{
    public Task<IEnumerable<Speaker>> GetAllSpeakers();
    public Task<CreateSpeakerDto> CreateSpeaker(CreateSpeakerDto createSpeakerDto);
}
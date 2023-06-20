using Meetup.Application.Dtos;
using Meetup.Models;

namespace Meetup.Services;

public interface IEventDataClient
{
    public Task<IEnumerable<EventInformation>> GetAllEvents();
    public Task<EventInformation> GetEventById(Guid id);
    public Task<CreateEventDto> CreateEvent(CreateEventDto eventDto);
    public Task<UpdateEventDto> UpdateEvent(UpdateEventDto eventDto);
    public Task DeleteEvent(DeleteEventDto eventDto);
}
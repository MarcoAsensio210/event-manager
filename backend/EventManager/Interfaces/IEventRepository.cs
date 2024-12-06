using EventManager.Models;

namespace EventManager.Interfaces
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetAllEventsAsync();
        Task<Event?> GetEventByIdAsync(int id);
        Task<Event> AddEventAsync(Event newEvent);
        Task<Event?> UpdateEventAsync(int id, Event updatedEvent);
        Task<bool> DeleteEventAsync(int id);
        Task<IEnumerable<Event>> GetEventsByVenueIdAsync(int venueId);
    }
}

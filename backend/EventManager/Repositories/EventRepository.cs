using EventManager.Data;
using EventManager.Interfaces;
using EventManager.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace EventManager.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly AppDbContext _context;

        public EventRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            return await _context.Events.Include(e => e.Venue).ToListAsync();
        }

        public async Task<Event?> GetEventByIdAsync(int id)
        {
            return await _context.Events.Include(e => e.Venue).FirstOrDefaultAsync(e => e.EventID == id);
        }

        public async Task<Event> AddEventAsync(Event newEvent)
        {
            _context.Events.Add(newEvent);
            await _context.SaveChangesAsync();
            return newEvent;
        }

        public async Task<Event?> UpdateEventAsync(int id, Event updatedEvent)
        {
            var existingEvent = await _context.Events.FindAsync(id);
            if (existingEvent == null)
                return null;

            existingEvent.Name = updatedEvent.Name;
            existingEvent.Date = updatedEvent.Date;
            existingEvent.VenueID = updatedEvent.VenueID;
            existingEvent.Description = updatedEvent.Description;
            existingEvent.Organizer = updatedEvent.Organizer;
            existingEvent.TicketPrice = updatedEvent.TicketPrice;

            await _context.SaveChangesAsync();
            return existingEvent;
        }

        public async Task<bool> DeleteEventAsync(int id)
        {
            var existingEvent = await _context.Events.FindAsync(id);
            if (existingEvent == null)
                return false;

            _context.Events.Remove(existingEvent);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<Event>> GetEventsByVenueIdAsync(int venueId)
        {
            return await _context.Events
                .Where(e => e.VenueID == venueId)
                .Include(e => e.Venue) 
                .ToListAsync();
        }
    }
}

using EventManager.Data;
using EventManager.Interfaces;
using EventManager.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace EventManager.Repositories
{
    public class VenueRepository : IVenueRepository
    {
        private readonly AppDbContext _context;

        public VenueRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Venue>> GetAllVenuesAsync()
        {
            return await _context.Venues.ToListAsync();
        }

        public async Task<Venue?> GetVenueByIdAsync(int id)
        {
            return await _context.Venues.FindAsync(id);
        }

        public async Task<Venue> AddVenueAsync(Venue venue)
        {
            _context.Venues.Add(venue);
            await _context.SaveChangesAsync();
            return venue;
        }

        public async Task<Venue?> UpdateVenueAsync(int id, Venue venue)
        {
            var existingVenue = await _context.Venues.FindAsync(id);
            if (existingVenue == null)
                return null;

            existingVenue.Name = venue.Name;
            existingVenue.Address = venue.Address;
            existingVenue.City = venue.City;
            existingVenue.State = venue.State;
            existingVenue.Capacity = venue.Capacity;
            existingVenue.ContactNumber = venue.ContactNumber;
            existingVenue.Email = venue.Email;

            await _context.SaveChangesAsync();
            return existingVenue;
        }

        public async Task<bool> DeleteVenueAsync(int id)
        {
            var venue = await _context.Venues.FindAsync(id);
            if (venue == null)
                return false;

            _context.Venues.Remove(venue);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

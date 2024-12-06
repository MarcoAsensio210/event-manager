using EventManager.Models;


namespace EventManager.Interfaces
{
    public interface IVenueRepository
    {
        Task<IEnumerable<Venue>> GetAllVenuesAsync();
        Task<Venue?> GetVenueByIdAsync(int id);
        Task<Venue> AddVenueAsync(Venue venue);
        Task<Venue?> UpdateVenueAsync(int id, Venue venue);
        Task<bool> DeleteVenueAsync(int id);
    }
}

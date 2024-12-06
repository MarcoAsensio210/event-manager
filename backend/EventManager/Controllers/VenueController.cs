using EventManager.DTOs;
using EventManager.Interfaces;
using EventManager.Models;

using Microsoft.AspNetCore.Mvc;

namespace EventManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VenueController : ControllerBase
    {
        private readonly IVenueRepository _repository;

        public VenueController(IVenueRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VenueDTO>>> GetAllVenues()
        {
            var venues = await _repository.GetAllVenuesAsync();
            var venueDTOs = venues.Select(v => new VenueDTO
            {
                VenueID = v.VenueID,
                Name = v.Name,
                Address = v.Address,
                City = v.City,
                State = v.State,
                Capacity = v.Capacity,
                ContactNumber = v.ContactNumber,
                Email = v.Email
            });
            return Ok(venueDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VenueDTO>> GetVenue(int id)
        {
            var venue = await _repository.GetVenueByIdAsync(id);
            if (venue == null)
                return NotFound();

            var venueDTO = new VenueDTO
            {
                VenueID = venue.VenueID,
                Name = venue.Name,
                Address = venue.Address,
                City = venue.City,
                State = venue.State,
                Capacity = venue.Capacity,
                ContactNumber = venue.ContactNumber,
                Email = venue.Email
            };
            return Ok(venueDTO);
        }

        [HttpPost]
        public async Task<ActionResult<VenueDTO>> AddVenue(VenueDTO venueDTO)
        {
            var venue = new Venue
            {
                Name = venueDTO.Name,
                Address = venueDTO.Address,
                City = venueDTO.City,
                State = venueDTO.State,
                Capacity = venueDTO.Capacity,
                ContactNumber = venueDTO.ContactNumber,
                Email = venueDTO.Email
            };

            venue = await _repository.AddVenueAsync(venue);

            return CreatedAtAction(nameof(GetVenue), new { id = venue.VenueID }, venueDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVenue(int id, VenueDTO venueDTO)
        {
            var venue = new Venue
            {
                Name = venueDTO.Name,
                Address = venueDTO.Address,
                City = venueDTO.City,
                State = venueDTO.State,
                Capacity = venueDTO.Capacity,
                ContactNumber = venueDTO.ContactNumber,
                Email = venueDTO.Email
            };

            var updatedVenue = await _repository.UpdateVenueAsync(id, venue);
            if (updatedVenue == null)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVenue(int id)
        {
            var success = await _repository.DeleteVenueAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}

using EventManager.DTOs;
using EventManager.Interfaces;
using EventManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventRepository _repository;

        public EventController(IEventRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventDTO>>> GetAllEvents()
        {
            var events = await _repository.GetAllEventsAsync();
            var eventDTOs = events.Select(e => new EventDTO
            {
                EventID = e.EventID,
                Name = e.Name,
                Date = e.Date,
                VenueID = e.VenueID,
                Description = e.Description,
                Organizer = e.Organizer,
                TicketPrice = e.TicketPrice
            });
            return Ok(eventDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EventDTO>> GetEvent(int id)
        {
            var eventItem = await _repository.GetEventByIdAsync(id);
            if (eventItem == null)
                return NotFound();

            var eventDTO = new EventDTO
            {
                EventID = eventItem.EventID,
                Name = eventItem.Name,
                Date = eventItem.Date,
                VenueID = eventItem.VenueID,
                Description = eventItem.Description,
                Organizer = eventItem.Organizer,
                TicketPrice = eventItem.TicketPrice
            };

            return Ok(eventDTO);
        }

        [HttpPost]
        public async Task<ActionResult<EventDTO>> AddEvent(EventDTO eventDTO)
        {
            var newEvent = new Event
            {
                Name = eventDTO.Name,
                Date = eventDTO.Date,
                VenueID = eventDTO.VenueID,
                Description = eventDTO.Description,
                Organizer = eventDTO.Organizer,
                TicketPrice = eventDTO.TicketPrice
            };

            newEvent = await _repository.AddEventAsync(newEvent);
            return CreatedAtAction(nameof(GetEvent), new { id = newEvent.EventID }, eventDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, EventDTO eventDTO)
        {
            var updatedEvent = new Event
            {
                Name = eventDTO.Name,
                Date = eventDTO.Date,
                VenueID = eventDTO.VenueID,
                Description = eventDTO.Description,
                Organizer = eventDTO.Organizer,
                TicketPrice = eventDTO.TicketPrice
            };

            var result = await _repository.UpdateEventAsync(id, updatedEvent);
            if (result == null)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var success = await _repository.DeleteEventAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
        [HttpGet("venue/{venueId}")]
        public async Task<ActionResult<IEnumerable<EventDTO>>> GetEventsByVenueId(int venueId)
        {
            var events = await _repository.GetEventsByVenueIdAsync(venueId);

            if (!events.Any())
                return NotFound($"No events found for venue ID {venueId}.");

            var eventDTOs = events.Select(e => new EventDTO
            {
                EventID = e.EventID,
                Name = e.Name,
                Date = e.Date,
                VenueID = e.VenueID,
                Description = e.Description,
                Organizer = e.Organizer,
                TicketPrice = e.TicketPrice
            });

            return Ok(eventDTOs);
        }

    }
}

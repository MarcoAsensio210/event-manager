using EventManager.Models;
using System.ComponentModel.DataAnnotations;

namespace EventManager.Models
{
    public class Event
    {
        public int EventID { get; set; }
        public string Name { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }


        public int VenueID { get; set; }
        public string? Description { get; set; }
        public string? Organizer { get; set; }
        public decimal? TicketPrice { get; set; }

        public Venue? Venue { get; set; } // Navigation property
    }
}

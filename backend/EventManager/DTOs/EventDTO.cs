namespace EventManager.DTOs
{
    public class EventDTO
    {
        public int EventID { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public int VenueID { get; set; }
        public string? Description { get; set; }
        public string? Organizer { get; set; }
        public decimal? TicketPrice { get; set; }
    }
}

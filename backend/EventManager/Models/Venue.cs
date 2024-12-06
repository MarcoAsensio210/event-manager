namespace EventManager.Models
{
    public class Venue
    {
        public int VenueID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public string? ContactNumber { get; set; }
        public string? Email { get; set; }
    }
}

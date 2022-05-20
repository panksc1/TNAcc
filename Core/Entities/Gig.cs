namespace Core.Entities
{
    public class Gig : BaseEntity
    {
        public DateTime Date { get; set; }
        public decimal Pay { get; set; }
        public int VenueId { get; set; }
        public Venue Venue { get; set; }
    }
}
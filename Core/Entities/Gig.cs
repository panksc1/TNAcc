namespace Core.Entities
{
    public class Gig : BaseEntity
    {
        public DateTime Date { get; set; }
        public decimal Pay { get; set; }
        public int VenueId { get; set; }
        public Venue Venue { get; set; }
        public int BandId { get; set; }
        public Band Band { get; set; }
        public string Notes { get; set; }
    }
}
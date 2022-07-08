namespace API.Dtos
{
    public class GigDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Pay { get; set; }
        public int VenueId { get; set; }
        public string Venue { get; set; }
        public int BandId { get; set; }
        public string Band { get; set; }
        public string Notes { get; set; }

        public bool IsValid() 
        {
            return VenueId > 0 && BandId > 0 && Date != DateTime.MinValue;
        }
    }
}
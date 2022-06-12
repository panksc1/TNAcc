namespace API.Dtos
{
    public class GigDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Pay { get; set; }
        public string Venue { get; set; }
        public string Band { get; set; }
    }
}
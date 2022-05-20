namespace API.Dtos
{
    public class PayableDto
    {
        public decimal Amount { get; set; }
        public DateTime DatePaid { get; set; }
        public string Entity { get; set; }
        public GigDto Gig { get; set; }  
    }
}
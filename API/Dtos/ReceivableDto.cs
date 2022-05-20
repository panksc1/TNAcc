namespace API.Dtos
{
    public class ReceivableDto
    {
        public decimal Amount { get; set; } 
        public DateTime DateReceived { get; set; }
        public string Entity { get; set; }
        public GigDto Gig { get; set; }  
    }
}
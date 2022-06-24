namespace API.Dtos
{
    public class ReceivableDto
    {
        public int Id { get; set; }
        public decimal AmountDue { get; set; }
        public decimal AmountPaid { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime DateReceived { get; set; }
        public string Method { get; set; }
        public string Notes { get; set; }
        public string Entity { get; set; }
        public GigDto Gig { get; set; }
    }
}
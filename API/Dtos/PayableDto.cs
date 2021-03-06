namespace API.Dtos
{
    public class PayableDto
    {
        public int Id { get; set; }
        public decimal AmountDue { get; set; }
        public decimal AmountPaid { get; set; }
        public DateTime DatePaid { get; set; }
        public string Method { get; set; }
        public string Notes { get; set; }
        public int EntityId { get; set; }
        public string Entity { get; set; }
        public GigDto Gig { get; set; }

        public bool IsValid()
        {
            return EntityId > 0 && Gig != null && Gig.Id > 0;
        }
    }
}
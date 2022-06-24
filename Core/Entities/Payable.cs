namespace Core.Entities
{
    public class Payable : BaseEntity
    {
        public decimal AmountDue { get; set; }
        public decimal AmountPaid { get; set; }
        public DateTime DatePaid { get; set; }
        public string Method { get; set; }
        public string Notes { get; set; }
        public int EntityId { get; set; }
        public Entity Entity { get; set; }
        public int GigId { get; set; }
        public Gig Gig { get; set; }
    }
}
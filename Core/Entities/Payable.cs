namespace Core.Entities
{
    public class Payable : BaseEntity
    {
        public decimal Amount { get; set; }
        public DateTime DatePaid { get; set; }
        public int EntityId { get; set; }
        public Entity Entity { get; set; }
        public int GigId { get; set; }
        public Gig Gig { get; set; }
    }
}
namespace Core.Entities
{
    public class Receivable : BaseEntity
    {
        public decimal Amount { get; set; } 
        public DateTime DateReceived { get; set; }
        public int EntityId { get; set; }
        public Entity Entity { get; set; }
        public int GigId { get; set; }
        public Gig Gig { get; set; }  
    }
}
namespace Core.Entities
{
    public class Venue : BaseEntity
    {
        public string Name { get; set; }
        public string At { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
}
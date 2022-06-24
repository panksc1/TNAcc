namespace Core.Entities
{
    public class Entity : BaseEntity
    {
        public string Company { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Notes { get; set; }
        public string Type { get; set; }
    }
}
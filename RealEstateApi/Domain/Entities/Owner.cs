namespace RealEstateApi.Domain.Entities
{
    public class Owner
    {
        public Guid IdOwner { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Photo { get; set; }
        public DateTime Birthday { get; set; }

        // Navegación inversa
        public ICollection<Property> Properties { get; set; } = new List<Property>();
    }
}


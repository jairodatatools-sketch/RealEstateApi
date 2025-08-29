namespace RealEstateApi.Domain.Entities
{
    public class PropertyImage
    {
        public Guid IdPropertyImage { get; set; }
        public Guid IdProperty { get; set; }
        public string FilePath { get; set; }
        public bool Enabled { get; set; }

        public Property Property { get; set; }
    }
}

namespace RealEstateApi.Application.DTOs
{
    public class PropertyImageDto
    {
        public Guid IdPropertyImage { get; set; }
        public Guid IdProperty { get; set; } // útil para vincular la imagen
        public string FilePath { get; set; }
        public bool Enabled { get; set; }
    }
}

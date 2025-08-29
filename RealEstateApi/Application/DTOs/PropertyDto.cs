namespace RealEstateApi.Application.DTOs
{
    namespace RealEstateApi.Application.DTOs
    {
        public class PropertyDto
        {
            public Guid IdProperty { get; set; }
            public string Name { get; set; }
            public string? Address { get; set; }
            public decimal Price { get; set; }
            public string? CodeInternal { get; set; }

            public int? Year { get; set; }

            public Guid IdOwner { get; set; }

            // Imágenes como URLs
            public List<string>? ImageUrls { get; set; }

            // Timestamps (opcional pero útil)
            public DateTime CreatedAt { get; set; }
            public DateTime? UpdatedAt { get; set; }
        }
    }

}

namespace RealEstateApi.Application.DTOs
{
    namespace RealEstateApi.Application.DTOs
    {
        public class UpdatePropertyDto
        {
            public string? Name { get; set; }
            public string? Address { get; set; }
            public decimal? Price { get; set; }
            public string? CodeInternal { get; set; }
            public int? Year { get; set; }
            public Guid? IdOwner { get; set; }
            public DateTime? UpdatedAt { get; set; }

            // Solo lectura
            public List<string>? ImageUrls { get; set; }
        }

    }
}



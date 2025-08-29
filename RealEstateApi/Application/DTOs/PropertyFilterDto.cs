namespace RealEstateApi.Application.DTOs
{
    public class PropertyFilterDto
    {
        public string? City { get; set; }
        public string? Country { get; set; }

        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }

        public Guid? IdOwner { get; set; }

        // Para búsquedas por nombre o descripción
        public string? Keyword { get; set; }

        // Paginación (opcional)
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}


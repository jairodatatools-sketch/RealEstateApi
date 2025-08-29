using RealEstateApi.Application.DTOs;
using RealEstateApi.Application.DTOs.RealEstateApi.Application.DTOs;

namespace RealEstateApi.Application.Interfaces
{
    public interface IPropertyService
    {
        Task<IEnumerable<PropertyDto>> GetAllAsync();
        Task<PropertyDto?> GetByIdAsync(Guid id);
        Task<Guid> CreateAsync(PropertyDto dto);
        Task UpdateAsync(Guid id, UpdatePropertyDto dto);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<PropertyDto>> GetByOwnerIdAsync(Guid ownerId);
        Task<bool> ExistsAsync(Guid id);
        Task AddImageAsync(Guid propertyId, PropertyImageDto imageDto);
        Task ChangePriceAsync(Guid propertyId, decimal newPrice);
        Task<IEnumerable<PropertyDto>> FilterAsync(PropertyFilterDto filter);
    }
}

using RealEstateApi.Application.DTOs;
using RealEstateApi.Domain.Entities;

namespace RealEstateApi.Domain.Interfaces
{
    public interface IPropertyRepository
    {
        Task<IEnumerable<Property>> GetAllAsync();
        Task<Property?> GetByIdAsync(Guid id);
        Task AddAsync(Property property);
        Task UpdateAsync(Property property);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task<IEnumerable<Property>> GetByOwnerIdAsync(Guid ownerId);
        Task<Guid> CreateAsync(Property property);
        Task AddImageAsync(Guid propertyId, PropertyImage image);
        Task ChangePriceAsync(Guid propertyId, decimal newPrice);
        Task<IEnumerable<Property>> FilterAsync(PropertyFilterDto filter);

    }
}


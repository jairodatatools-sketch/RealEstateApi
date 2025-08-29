using Microsoft.EntityFrameworkCore;

using RealEstateApi.Application.DTOs;
using RealEstateApi.Domain.Entities;
using RealEstateApi.Domain.Interfaces;
using RealEstateApi.Infrastructure.Persistence;

namespace RealEstateApi.Infrastructure.Repositories
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PropertyRepository> _logger;

        public PropertyRepository(ApplicationDbContext context, ILogger<PropertyRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Property>> GetAllAsync()
        {
            return await _context.Properties
                .Include(p => p.Images)
                .Include(p => p.Owner)
                .ToListAsync();
        }

        public async Task<Property?> GetByIdAsync(Guid id)
        {
            return await _context.Properties
                .Include(p => p.Images)
                .Include(p => p.Owner)
                .FirstOrDefaultAsync(p => p.IdProperty == id);
        }

        public async Task AddAsync(Property property)
        {
            await _context.Properties.AddAsync(property);
            await _context.SaveChangesAsync();
        }

        public async Task<Guid> CreateAsync(Property property)
        {
            await _context.Properties.AddAsync(property);
            await _context.SaveChangesAsync();
            return property.IdProperty;
        }

        public async Task UpdateAsync(Property property)
        {
            _context.Properties.Update(property);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var property = await _context.Properties.FirstOrDefaultAsync(p => p.IdProperty == id);
            if (property != null)
            {
                _context.Properties.Remove(property);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Properties.AnyAsync(p => p.IdProperty == id);
        }

        public async Task<IEnumerable<Property>> GetByOwnerIdAsync(Guid ownerId)
        {
            return await _context.Properties
                .Where(p => p.IdOwner == ownerId)
                .Include(p => p.Images)
                .ToListAsync();
        }

        public async Task AddImageAsync(Guid propertyId, PropertyImage image)
        {
            //image.IdPropertyImage = Guid.NewGuid();
            await _context.PropertyImages.AddAsync(image);
            await _context.SaveChangesAsync();
        }

        public async Task ChangePriceAsync(Guid propertyId, decimal newPrice)
        {
            var property = await _context.Properties.FirstOrDefaultAsync(p => p.IdProperty == propertyId);
            if (property != null)
            {
                property.Price = newPrice;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Property>> FilterAsync(PropertyFilterDto filter)
        {
            _logger.LogInformation("Inicio de filtrado de propiedades con parámetros: {@Filter}", filter);

            var query = _context.Properties
                .Include(p => p.Images)
                .Include(p => p.Owner)
                .AsQueryable();

            if (filter.IdOwner.HasValue)
            {
                _logger.LogInformation("Aplicando filtro por IdOwner: {IdOwner}", filter.IdOwner);
                query = query.Where(p => p.IdOwner == filter.IdOwner.Value);
            }

            if (filter.MinPrice.HasValue)
            {
                _logger.LogInformation("Aplicando filtro por MinPrice: {MinPrice}", filter.MinPrice);
                query = query.Where(p => p.Price >= filter.MinPrice.Value);
            }

            if (filter.MaxPrice.HasValue)
            {
                _logger.LogInformation("Aplicando filtro por MaxPrice: {MaxPrice}", filter.MaxPrice);
                query = query.Where(p => p.Price <= filter.MaxPrice.Value);
            }

            if (filter.PageNumber.HasValue && filter.PageSize.HasValue)
            {
                int skip = (filter.PageNumber.Value - 1) * filter.PageSize.Value;
                _logger.LogInformation("Aplicando paginación: PageNumber={PageNumber}, PageSize={PageSize}, Skip={Skip}", filter.PageNumber, filter.PageSize, skip);
                query = query.Skip(skip).Take(filter.PageSize.Value);
            }
            var result = await query.ToListAsync();

            _logger.LogInformation("Filtrado completado. Total propiedades encontradas: {Count}", result.Count);

            return result;
        }
    }
}

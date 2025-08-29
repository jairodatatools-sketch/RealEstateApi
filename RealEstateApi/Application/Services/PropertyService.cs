using AutoMapper;

using RealEstateApi.Application.DTOs;
using RealEstateApi.Application.DTOs.RealEstateApi.Application.DTOs;
using RealEstateApi.Application.Interfaces;
using RealEstateApi.Domain.Entities;
using RealEstateApi.Domain.Interfaces;

namespace RealEstateApi.Application.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository _repository;
        private readonly IMapper _mapper;

        public PropertyService(IPropertyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PropertyDto>> GetAllAsync()
        {
            var properties = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<PropertyDto>>(properties);
        }

        public async Task<PropertyDto?> GetByIdAsync(Guid id)
        {
            var property = await _repository.GetByIdAsync(id);
            return property == null ? null : _mapper.Map<PropertyDto>(property);
        }

        public async Task<Guid> CreateAsync(PropertyDto dto)
        {
            var entity = _mapper.Map<Property>(dto);
            return await _repository.CreateAsync(entity);
        }

        public async Task UpdateAsync(Guid id, UpdatePropertyDto dto)
        {
            //var existing = await _repository.GetByIdAsync(id);
            //if (existing == null) return;

            //_mapper.Map(dto, existing);
            //await _repository.UpdateAsync(existing);

            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return;

            _mapper.Map(dto, existing); // ya no toca IdProperty
            await _repository.UpdateAsync(existing);

        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<PropertyDto>> GetByOwnerIdAsync(Guid ownerId)
        {
            var properties = await _repository.GetByOwnerIdAsync(ownerId);
            return _mapper.Map<IEnumerable<PropertyDto>>(properties);
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _repository.ExistsAsync(id);
        }

        public async Task AddImageAsync(Guid propertyId, PropertyImageDto imageDto)
        {
            var image = _mapper.Map<PropertyImage>(imageDto);
            await _repository.AddImageAsync(propertyId, image);
        }

        public async Task ChangePriceAsync(Guid propertyId, decimal newPrice)
        {
            await _repository.ChangePriceAsync(propertyId, newPrice);
        }

        public async Task<IEnumerable<PropertyDto>> FilterAsync(PropertyFilterDto filter)
        {
            var filtered = await _repository.FilterAsync(filter);
            return _mapper.Map<IEnumerable<PropertyDto>>(filtered);
        }
    }
}

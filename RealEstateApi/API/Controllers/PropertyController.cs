using Microsoft.AspNetCore.Mvc;

using RealEstateApi.Application.DTOs;
using RealEstateApi.Application.DTOs.RealEstateApi.Application.DTOs;
using RealEstateApi.Application.Interfaces;

namespace RealEstateApi.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _service;

        public PropertyController(IPropertyService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var properties = await _service.GetAllAsync();
            return Ok(properties);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var property = await _service.GetByIdAsync(id);
            return property == null ? NotFound() : Ok(property);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PropertyDto dto)
        {
            var id = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdatePropertyDto dto)
        {
            await _service.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("owner/{ownerId}")]
        public async Task<IActionResult> GetByOwner(Guid ownerId)
        {
            var properties = await _service.GetByOwnerIdAsync(ownerId);
            return Ok(properties);
        }

        [HttpPost("{id}/images")]
        public async Task<IActionResult> AddImage(Guid id, PropertyImageDto imageDto)
        {
            await _service.AddImageAsync(id, imageDto);
            return NoContent();
        }

        [HttpPatch("{id}/price")]
        public async Task<IActionResult> ChangePrice(Guid id, [FromBody] decimal newPrice)
        {
            await _service.ChangePriceAsync(id, newPrice);
            return NoContent();
        }

        [HttpPost("filter")]
        public async Task<IActionResult> Filter(PropertyFilterDto filter)
        {
            var result = await _service.FilterAsync(filter);
            return Ok(result);
        }
    }
}


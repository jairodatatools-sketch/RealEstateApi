using AutoMapper;

using Moq;

using RealEstateApi.Application.DTOs;
using RealEstateApi.Application.DTOs.RealEstateApi.Application.DTOs;
using RealEstateApi.Application.Services;
using RealEstateApi.Domain.Entities;
using RealEstateApi.Domain.Interfaces;

[TestFixture]
public class PropertyServiceFilterTests
{
    private Mock<IPropertyRepository> _repositoryMock;
    private IMapper _mapper;
    private PropertyService _service;

    [SetUp]
    public void Setup()
    {
        _repositoryMock = new Mock<IPropertyRepository>();

        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Property, PropertyDto>();
        });

        _mapper = config.CreateMapper();
        _service = new PropertyService(_repositoryMock.Object, _mapper);
    }

    [Test]
    public async Task FilterAsync_WithValidPriceRange_ReturnsFilteredProperties()
    {
        var filter = new PropertyFilterDto
        {
            MinPrice = 100000m,
            MaxPrice = 300000m
        };

        var properties = new List<Property>
        {
            new Property
            {
                IdProperty = Guid.NewGuid(),
                Name = "Apartamento Norte",
                Address = "Calle 123",
                Price = 150000m,
                CodeInternal = "INT-001",
                Year = 2020,
                IdOwner = Guid.NewGuid()
            },
            new Property
            {
                IdProperty = Guid.NewGuid(),
                Name = "Casa Sur",
                Address = "Carrera 45",
                Price = 250000m,
                CodeInternal = "INT-002",
                Year = 2019,
                IdOwner = Guid.NewGuid()
            }
        };

        _repositoryMock
            .Setup(r => r.FilterAsync(filter))
            .ReturnsAsync(properties);

        var result = await _service.FilterAsync(filter);

        Assert.That(result.Count(), Is.EqualTo(2));
        Assert.IsTrue(result.All(p => p.Price >= 100000m && p.Price <= 300000m));
    }

    [Test]
    public async Task FilterAsync_WithNoMatchingProperties_ReturnsEmptyList()
    {
        var filter = new PropertyFilterDto
        {
            MinPrice = 900000m,
            MaxPrice = 1000000m
        };

        _repositoryMock
            .Setup(r => r.FilterAsync(filter))
            .ReturnsAsync(new List<Property>());

        var result = await _service.FilterAsync(filter);

        Assert.IsNotNull(result);
        Assert.That(result.Count(), Is.EqualTo(0));
    }

    [Test]
    public async Task FilterAsync_WithNullFilter_ReturnsAllProperties()
    {
        var allProperties = new List<Property>
        {
            new Property
            {
                IdProperty = Guid.NewGuid(),
                Name = "Casa Centro",
                Address = "Av. Caracas",
                Price = 200000m,
                CodeInternal = "INT-003",
                Year = 2018,
                IdOwner = Guid.NewGuid()
            }
        };

        _repositoryMock
            .Setup(r => r.FilterAsync(null))
            .ReturnsAsync(allProperties);

        var result = await _service.FilterAsync(null);

        Assert.That(result.Count(), Is.EqualTo(1));
    }

    [Test]
    public async Task FilterAsync_MapsEntitiesToDtosCorrectly()
    {
        var filter = new PropertyFilterDto { MinPrice = 100000m };

        var property = new Property
        {
            IdProperty = Guid.NewGuid(),
            Name = "Casa A",
            Address = "Calle 10",
            Price = 150000m,
            CodeInternal = "INT-004",
            Year = 2021,
            IdOwner = Guid.NewGuid()
        };

        _repositoryMock
            .Setup(r => r.FilterAsync(filter))
            .ReturnsAsync(new List<Property> { property });

        var result = await _service.FilterAsync(filter);

        Assert.That(result.Count(), Is.EqualTo(1));
        Assert.That(result.First().Name, Is.EqualTo("Casa A"));

        Assert.That(result.First().Price, Is.EqualTo(150000m));
        Assert.That(result.First().CodeInternal, Is.EqualTo("INT-004"));
    }
}


using AutoMapper;
using Moq;
using RealEstateApi.Application.DTOs.RealEstateApi.Application.DTOs;
using RealEstateApi.Application.Mappings;
using RealEstateApi.Application.Services;
using RealEstateApi.Domain.Entities;
using RealEstateApi.Domain.Interfaces;

[TestFixture]
public class PropertyServiceCreateTests
{
    private PropertyService _serviceC;
    private Mock<IPropertyRepository> _repoMock;
    private IMapper _mapperC;

    [SetUp]
    public void Setup()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<PropertyProfile>());
        _mapperC = config.CreateMapper();
        _repoMock = new Mock<IPropertyRepository>();
        _serviceC = new PropertyService(_repoMock.Object, _mapperC);
    }

    [Test]
    public async Task CreateAsync_ShouldReturnGuid_WhenValid()
    {
        var dto = new PropertyDto { Name = "Casa", Price = 1000000, IdOwner = Guid.NewGuid() };
        var expectedId = Guid.NewGuid();

        _repoMock.Setup(r => r.CreateAsync(It.IsAny<Property>())).ReturnsAsync(expectedId);

        var result = await _serviceC.CreateAsync(dto);

        Assert.That(result, Is.EqualTo(expectedId));
    }
}


using System.Net;

using FluentAssertions;

using Moq;

using RealEstateApi.Application.DTOs.RealEstateApi.Application.DTOs;

[TestFixture]
public class PropertyControllerTests
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Structure", "NUnit1032:An IDisposable field/property should be Disposed in a TearDown method", Justification = "<pendiente>")]
    private CustomWebApplicationFactory _factory = null!;
    private HttpClient _client = null!;

    [SetUp]
    public void Setup()
    {
        _factory = new CustomWebApplicationFactory();
        _client = _factory.CreateClient();
    }

    [Test]
    public async Task GetAll_ShouldReturnOkWithMockedData()
    {
        // Arrange: configura el mock
        var mockData = new List<PropertyDto>
        {
            new() { IdOwner = Guid.NewGuid(), Name = "Casa en Medellín", Price = 300000000 },
            new() { IdOwner = Guid.NewGuid(), Name = "Apartamento en Bogotá", Price = 450000000 }
        };

        _factory.PropertyServiceMock
            .Setup(s => s.GetAllAsync())
            .ReturnsAsync(mockData);

        // Act
        var response = await _client.GetAsync("/api/property");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain("Casa en Medellín").And.Contain("Apartamento en Bogotá");
    }
}


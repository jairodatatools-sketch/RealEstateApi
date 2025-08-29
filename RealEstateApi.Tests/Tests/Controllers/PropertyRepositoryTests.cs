using Microsoft.EntityFrameworkCore;

using RealEstateApi.Domain.Entities;
using RealEstateApi.Infrastructure.Persistence;
using RealEstateApi.Infrastructure.Repositories;

[TestFixture]
public class PropertyRepositoryTests
{
    private DbContextOptions<ApplicationDbContext> _options;

    [SetUp]
    public void Setup()
    {
        _options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }

    [Test]
    public async Task AddImageAsync_ShouldAddImageToProperty()
    {
        // Arrange
        using var context = new ApplicationDbContext(_options);

        var propertyId = Guid.NewGuid();
        var property = new Property
        {
            IdProperty = propertyId,
            Name = "Casa de prueba",
            Images = new System.Collections.Generic.List<PropertyImage>()
        };

        context.Properties.Add(property);
        await context.SaveChangesAsync();

        var image = new PropertyImage
        {
            FilePath = "https://example.com/image.jpg",
            Enabled = true
        };


        // Assert
        var updatedProperty = await context.Properties
            .Include(p => p.Images)
            .FirstOrDefaultAsync(p => p.IdProperty == propertyId);

        Assert.IsNotNull(updatedProperty);
        Assert.That(updatedProperty.Images.Count, Is.EqualTo(1));
        Assert.That(updatedProperty.Images.First().IdPropertyImage, Is.Not.EqualTo(Guid.Empty));
        Assert.That(updatedProperty.Images.First().FilePath, Is.EqualTo("https://example.com/image.jpg"));
    }
}



using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using RealEstateApi.Application.Interfaces;
using RealEstateApi.Application.DTOs;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    public Mock<IPropertyService> PropertyServiceMock { get; } = new();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Elimina la implementación real
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(IPropertyService));
            if (descriptor != null)
                services.Remove(descriptor);

            // Inyecta el mock
            services.AddSingleton(PropertyServiceMock.Object);
        });
    }
}

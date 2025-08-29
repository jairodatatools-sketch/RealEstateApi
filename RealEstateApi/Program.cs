using FluentValidation;
using FluentValidation.AspNetCore;

using Microsoft.EntityFrameworkCore;

using RealEstateApi.Application.Interfaces;
using RealEstateApi.Application.Mappings;
using RealEstateApi.Application.Services;
using RealEstateApi.Application.Validators;
using RealEstateApi.Domain.Interfaces;
using RealEstateApi.Infrastructure.Persistence;
using RealEstateApi.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPropertyService, PropertyService>();
builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddAutoMapper(typeof(PropertyProfile).Assembly);
builder.Services.AddValidatorsFromAssemblyContaining<PropertyDtoValidator>();
builder.Services.AddFluentValidationAutoValidation(); // activa validación automática en controllers




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { } // <- necesario para WebApplicationFactory

using FluentValidation;

using RealEstateApi.Application.DTOs.RealEstateApi.Application.DTOs;

namespace RealEstateApi.Application.Validators
{
    public class PropertyDtoValidator : AbstractValidator<PropertyDto>
    {
        public PropertyDtoValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(100);

            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage("El precio debe ser mayor a cero.");

            RuleFor(p => p.IdOwner)
                .NotEmpty().WithMessage("Debe especificarse el propietario.");
        }
    }
}


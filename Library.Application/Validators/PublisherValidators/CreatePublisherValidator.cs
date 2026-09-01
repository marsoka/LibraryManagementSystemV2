using FluentValidation;
using Library.Application.DTOs.PublisherDtos;



public class CreatePublisherValidator : AbstractValidator<CreatePublisherDto>
{
    public CreatePublisherValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Publisher name is required.")
            .MaximumLength(150)
            .WithMessage("Publisher name cannot exceed 150 characters.")
            .Matches(@"^[a-zA-Z\s]+$")
                .WithMessage("Publisher name can contain letters only.");

        RuleFor(x => x.Address)
            .NotEmpty()
            .WithMessage("Publisher Address is required.")
            .MaximumLength(300)
            .WithMessage("Address cannot exceed 300 characters.");

        RuleFor(x => x.Phone)
            .NotEmpty()
            .WithMessage("Publisher Phone is required.")
            .Matches(@"^\+?[1-9]\d{1,14}$")
            .WithMessage("Invalid Phone.")
            .MaximumLength(30)
            .WithMessage("Phone number cannot exceed 30 characters.");



    }
}
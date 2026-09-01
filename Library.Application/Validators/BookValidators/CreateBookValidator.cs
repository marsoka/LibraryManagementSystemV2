using FluentValidation;
using Library.Application.DTOs.BookDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.Validators.BookValidators
{
    public class CreateBookValidator : AbstractValidator<CreateBookDto>
    {
        public CreateBookValidator() 
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                    .WithMessage("Book title is required.")
                .MaximumLength(250)
                    .WithMessage("Book title cannot exceed 250 characters.");

            RuleFor(x => x.ISBN)
                .NotEmpty()
                    .WithMessage("Book ISBN is required.")
                .Matches(@"^\d{20}$")
                    .WithMessage("ISBN must contain exactly 20 digits.");

            RuleFor(x => x.PublicationYear)
                .InclusiveBetween(1500, DateTime.UtcNow.Year)
                    .WithMessage("Publication year must be in the past.");

            RuleFor(x => x.TotalCopies)
                .GreaterThan(0)
                    .WithMessage("Total copies must be graeter than 0.");

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0)
                    .WithMessage("Price must be graeter than or equal to 0.");

            RuleFor(x => x.AuthorId)
                .GreaterThan(0)
                    .WithMessage("Author ID must be graeter than 0.");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0)
                    .WithMessage("Category ID must be graeter than 0.");

            RuleFor(x => x.PublisherId)
                .GreaterThan(0)
                    .WithMessage("Publisher ID must be graeter than 0.");
        }
    }
}

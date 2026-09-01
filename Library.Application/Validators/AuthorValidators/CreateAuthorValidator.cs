using FluentValidation;
using Library.Application.DTOs.AuthorDtos;
using System;
using System.Collections.Generic;
using System.Text;


public class CreateAuthorValidator : AbstractValidator<CreateAuthorDto>
{
    public CreateAuthorValidator()
    {
        RuleFor(a => a.FullName)
            .NotNull()
                .WithMessage("Name is required.")
            .NotEmpty()
                .WithMessage("Name is required.")
            .Matches(@"^[a-zA-Z\s]+$")
                .WithMessage("Name can contain letters only.")
            .MaximumLength(150)
                .WithMessage("Name cannot exceed 150 characters.");

        RuleFor(a => a.Biography)
            .NotNull()
                .WithMessage("Biography is required.")
            .NotEmpty()
                .WithMessage("Biography is required.");

        RuleFor(a => a.DateOfBirth)
            .NotNull()
                .WithMessage("Data Of Birth is required.")
            .NotEmpty()
                .WithMessage("Data Of Birth is required.")
            .InclusiveBetween(DateOnly.MinValue, DateOnly.FromDateTime(DateTime.Today))
                .WithMessage("Birth date must be in the past.");

        RuleFor(a => a.Nationality)
            .NotNull()
                .WithMessage("Nationality is required.")
            .NotEmpty()
                .WithMessage("Nationality is required.")
            .MaximumLength(100)
                .WithMessage("Nationality cannot exceed 100 characters.");

    }
}


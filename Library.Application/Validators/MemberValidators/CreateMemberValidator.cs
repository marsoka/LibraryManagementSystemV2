using FluentValidation;
using Library.Application.DTOs.BookDtos;
using Library.Application.DTOs.MemberDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.Validators.MemberValidators
{
    public class CreateMemberValidator : AbstractValidator<CreateMemberDto>
    {
        public CreateMemberValidator() 
        {
            RuleFor(x => x.FullName)
                .NotEmpty()
                .WithMessage("Full name is required.")
                .MaximumLength(150)
                .WithMessage("Full name cannot exceed 150 characters.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Invalid email address.")
                .MaximumLength(254)
                .WithMessage("Email cannot exceed 254 characters.");

            RuleFor(x => x.Phone)
                .NotEmpty()
                .WithMessage("Phone number is required.")
                .Matches(@"^\+?[1-9]\d{1,14}$")
                .WithMessage("Invalid phone number.")
                .MaximumLength(30)
                .WithMessage("Phone number cannot exceed 30 characters.");

            RuleFor(x => x.Address)
                .NotEmpty()
                .WithMessage("Address is required.")
                .MaximumLength(300)
                .WithMessage("Address cannot exceed 300 characters.");
        }
    }
}

using FluentValidation;
using Library.Application.DTOs.BorrowingDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.Validators.BorrowingValidators
{
    public class UpdateBorrowingValidator : AbstractValidator<UpdateBorrowingDto>
    {
        public UpdateBorrowingValidator()
        {
            RuleFor(x => x.BookId)
                .GreaterThan(0)
                .WithMessage("Book ID must be greater than 0.");

            RuleFor(x => x.MemberId)
                .GreaterThan(0)
                .WithMessage("Member ID must be greater than 0.");

        }
    }
}

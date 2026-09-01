using FluentValidation;
using Library.Application.DTOs.BookDtos;
using Library.Application.DTOs.BorrowingDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.Validators.BookValidators
{
    public class BorrowingQueryValidator : AbstractValidator<BorrowingQueryDto>
    {
        public BorrowingQueryValidator() 
        {
            RuleFor(x => x.MemberId)
                .GreaterThan(0);

            RuleFor(x => x.BookId)
                .GreaterThan(0);

            RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1);

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 100);

            RuleFor(x => x.MinBorrowDate)
                .GreaterThan(DateTime.MinValue)
                .When(x => x.MinBorrowDate.HasValue);

            RuleFor(x => x.MaxBorrowDate)
                .GreaterThan(DateTime.MinValue)
                .When(x => x.MaxBorrowDate.HasValue);

            RuleFor(x => x.MaxBorrowDate)
                .GreaterThanOrEqualTo(x => x.MinBorrowDate);

            RuleFor(x => x.MinDueDate)
                .GreaterThan(DateTime.MinValue)
                .When(x => x.MinDueDate.HasValue);

            RuleFor(x => x.MaxDueDate)
                .GreaterThan(DateTime.MinValue)
                .When(x => x.MaxDueDate.HasValue);

            RuleFor(x => x.MaxDueDate)
                .GreaterThanOrEqualTo(x => x.MinDueDate);

            RuleFor(x => x.MinReturnDate)
                .GreaterThan(DateTime.MinValue)
                .When(x => x.MinReturnDate.HasValue);

            RuleFor(x => x.MaxReturnDate)
                .GreaterThan(DateTime.MinValue)
                .When(x => x.MaxReturnDate.HasValue);

            RuleFor(x => x.MaxReturnDate)
                .GreaterThanOrEqualTo(x => x.MinReturnDate);

            RuleFor(x => x.Status)
                .IsInEnum();
        }
    }
}

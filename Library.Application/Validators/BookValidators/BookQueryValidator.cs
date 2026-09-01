using FluentValidation;
using Library.Application.DTOs.BookDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.Validators.BookValidators
{
    public class BookQueryValidator : AbstractValidator<BookQueryDto>
    {
        public BookQueryValidator() 
        {
            RuleFor(x => x.AuthorId)
                .GreaterThan(0);

            RuleFor(x => x.CategoryId)
                .GreaterThan(0);

            RuleFor(x => x.PublisherId)
                .GreaterThan(0);

            RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1);

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 100);

            RuleFor(x => x.MinYear)
                .GreaterThan(0)
                .When(x => x.MinYear.HasValue);

            RuleFor(x => x.MaxYear)
                .GreaterThan(0)
                .When(x => x.MaxYear.HasValue);

            RuleFor(x => x.MaxYear)
                .GreaterThanOrEqualTo(x => x.MinYear);

            RuleFor(x => x.MinPrice)
                .GreaterThanOrEqualTo(0)
                .When(x => x.MinPrice.HasValue);

            RuleFor(x => x.MaxPrice)
                .GreaterThanOrEqualTo(0)
                .When(x => x.MaxPrice.HasValue);

            RuleFor(x => x.MaxPrice)
                .GreaterThanOrEqualTo(x => x.MinPrice);
        }
    }
}

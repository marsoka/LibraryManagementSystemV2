using Library.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.DTOs.BookDtos
{
    public class BookQueryDto
    {
        public string? Search { get; set; }

        public int? AuthorId { get; set; }

        public int? CategoryId { get; set; }

        public int? PublisherId { get; set; }

        public int? MinYear { get; set; }

        public int? MaxYear { get; set; }

        public decimal? MinPrice { get; set; }

        public decimal? MaxPrice { get; set; }

        public bool? AvailableOnly { get; set; }

        public BookSortBy? SortBy { get; set; }

        public bool Descending { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}

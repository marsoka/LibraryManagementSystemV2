using Library.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.DTOs.BorrowingDtos
{
    public class BorrowingQueryDto
    {
        public int? MemberId { get; set; }
        public int? BookId { get; set; }
        public DateTime? MinBorrowDate { get; set; }
        public DateTime? MaxBorrowDate { get; set; }
        public DateTime? MinDueDate { get; set; }
        public DateTime? MaxDueDate { get; set; }
        public DateTime? MinReturnDate { get; set; }
        public DateTime? MaxReturnDate { get; set; }
        public BorrowingStatus? Status { get; set; }
        public BorrowingSortBy? SortBy { get; set; }
        public bool Descending { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}

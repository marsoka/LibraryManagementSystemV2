using Library.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.DTOs.BorrowingDtos
{
    public class BorrowingDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int MemberId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public BorrowingStatus Status { get; set; }
    }
}

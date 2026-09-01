using Library.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.DTOs.BorrowingDtos
{
    public class UpdateBorrowingDto
    {
        public int BookId { get; set; }
        public int MemberId { get; set; }
    }
}

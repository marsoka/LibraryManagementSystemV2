using Library.Domain.Enums;
using Library.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain.Entities
{
    public class Borrowing
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int MemberId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public BorrowingStatus Status { get; set; }

        public Book Book { get; set; }
        public Member Member { get; set; }


        public void Return(DateTime returnDate)
        {
            if (ReturnDate.HasValue)
                throw new BorrowingAlreadyReturnedException(Id);

            ReturnDate = returnDate;
            Status = BorrowingStatus.Returned;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain.Exceptions
{
    public class BorrowingAlreadyReturnedException : DomainException
    {
        public BorrowingAlreadyReturnedException(int id) 
            : base($"Borrowing with id {id} Already Returned.")
        {
        }
    }
}

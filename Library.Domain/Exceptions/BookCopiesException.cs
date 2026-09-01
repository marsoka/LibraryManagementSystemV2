using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain.Exceptions
{
    public class BookCopiesException : DomainException
    {
        public BookCopiesException(string message) 
            : base(message)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain.Exceptions
{
    public class InvalidBookCopiesException : DomainException
    {
        public InvalidBookCopiesException() 
            : base("Total copies must be graeter than 0.")
        {
        }
        public InvalidBookCopiesException(string message)
            : base(message)
        {
        }
    }
}

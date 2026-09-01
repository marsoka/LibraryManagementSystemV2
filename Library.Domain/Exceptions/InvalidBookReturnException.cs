using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain.Exceptions
{
    public class InvalidBookReturnException : DomainException
    {
        public InvalidBookReturnException() 
            : base("Invalid Book Return.")
        {
        }
    }
}

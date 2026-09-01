using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain.Exceptions
{
    public class NoAvailableCopiesException : DomainException
    {
        public NoAvailableCopiesException()
            : base("No copies of this book are available.")
        {
        }
    }
}

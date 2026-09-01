using Library.Application.Execptions.StatusCodesExeptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.Exceptions.NotFoundExceptions
{
    internal class BookNotFoundException : NotFoundException
    {
        public BookNotFoundException(int id)
            : base($"Book with id {id} not found.")
        {
        }
    }
}

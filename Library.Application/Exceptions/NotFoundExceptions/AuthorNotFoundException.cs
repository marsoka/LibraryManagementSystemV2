using Library.Application.Execptions.StatusCodesExeptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.Exceptions.NotFoundExceptions
{
    public class AuthorNotFoundException : NotFoundException
    {
        public AuthorNotFoundException(int id)
            : base($"Author with id {id} not found.")
        {
        }
    }
}

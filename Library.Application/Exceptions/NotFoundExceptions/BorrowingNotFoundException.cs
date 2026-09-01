using Library.Application.Execptions.StatusCodesExeptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.Exceptions.NotFoundExceptions
{
    internal class BorrowingNotFoundException : NotFoundException
    {
        public BorrowingNotFoundException(int id) 
            : base($"Borrowing with id {id} not found.") 
        {
        }
    }
}

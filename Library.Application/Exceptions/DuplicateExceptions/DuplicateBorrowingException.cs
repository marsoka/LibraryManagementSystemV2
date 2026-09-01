using Library.Application.Execptions.StatusCodesExeptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.Exceptions.DuplicateExceptions
{
    public class DuplicateBorrowingException : ConflictException
    {
        public DuplicateBorrowingException(int memberId, int bookId) 
            : base($"Member with id {memberId} is already borrowed book with id {bookId}.") 
        {

        }
    }
}

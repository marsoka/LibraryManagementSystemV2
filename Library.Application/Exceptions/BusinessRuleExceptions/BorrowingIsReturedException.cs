using Library.Application.Execptions.StatusCodesExeptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.Exceptions.BusinessRuleExceptions
{
    public class BorrowingIsReturedException : BusinessRuleException
    {
        public BorrowingIsReturedException(int id) 
            : base($"The Borrowing with id {id} is already returd.")
        {
        }
    }
}

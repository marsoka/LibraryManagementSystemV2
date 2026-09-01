using Library.Application.Execptions.StatusCodesExeptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.Exceptions.BusinessRuleExceptions
{
    public class BookHasHistoryException : BusinessRuleException
    {
        public BookHasHistoryException(int id) 
            : base($"The book with id {id} has history and can't delete it")
        {
        }
    }
}

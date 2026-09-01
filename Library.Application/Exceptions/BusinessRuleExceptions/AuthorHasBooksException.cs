using Library.Application.Execptions.StatusCodesExeptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.Exceptions.BusinessRuleExceptions
{
    public class AuthorHasBooksException : BusinessRuleException
    {
        public AuthorHasBooksException(int id) 
            : base($"Cannot delete a Author with {id} that contains books.")
        {
        }
    }
}

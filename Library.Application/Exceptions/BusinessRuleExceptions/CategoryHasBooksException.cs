using Library.Application.Execptions.StatusCodesExeptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.Exceptions.BusinessRuleExceptions
{
    public class CategoryHasBooksException : BusinessRuleException
    {
        public CategoryHasBooksException(int id) 
            : base($"Cannot delete a Category with {id} that contains books.")
        {
        }
    }
}

using Library.Application.Execptions.StatusCodesExeptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.Exceptions.BusinessRuleExceptions
{
    public class MemberHasHistoryException : BusinessRuleException
    {
        public MemberHasHistoryException(int id) 
            : base($"The member with id {id} has history and can't delete it")
        {
        }
    }
}

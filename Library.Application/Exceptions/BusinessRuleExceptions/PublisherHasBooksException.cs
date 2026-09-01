using Library.Application.Execptions.StatusCodesExeptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.Exceptions.BusinessRuleExceptions
{
    public class PublisherHasBooksException : BusinessRuleException
    {
        public PublisherHasBooksException(int id) 
            : base($"Cannot delete a Publisher with {id} that contains books.")
        {
        }
    }
}

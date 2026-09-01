using Library.Application.Execptions.StatusCodesExeptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.Exceptions.NotFoundExceptions
{
    internal class MemberNotFoundException : NotFoundException
    {
        public MemberNotFoundException(int id) 
            : base($"Member with id {id} not found.") 
        {
        }
    }
}

using Library.Application.Execptions.StatusCodesExeptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.Exceptions.ExistExceptions
{
    internal class EmailExistException : ConflictException
    {
        public EmailExistException(string email)
            : base($"The email : {email} is already exist in system.")
        {
        }
    }
}

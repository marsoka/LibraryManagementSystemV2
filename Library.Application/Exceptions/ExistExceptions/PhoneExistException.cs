using Library.Application.Execptions.StatusCodesExeptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.Exceptions.ExistExceptions
{
    internal class PhoneExistException : ConflictException
    {
        public PhoneExistException(string phone)
            : base($"The email : {phone} is already exist in system.")
        {
        }
    }
}

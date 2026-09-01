using Library.Application.Execptions.StatusCodesExeptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.Exceptions.ExistExceptions
{
    internal class UsernameExistException : ConflictException
    {
        public UsernameExistException(string username) 
            : base($"The username : {username} is already exist in system.")
        {
        }
    }
}

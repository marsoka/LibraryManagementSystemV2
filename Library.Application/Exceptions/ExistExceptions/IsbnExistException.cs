using Library.Application.Execptions.StatusCodesExeptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.Exceptions.ExistExceptions
{
    internal class IsbnExistException : ConflictException
    {
        public IsbnExistException(string isbn)
            : base($"The ISBN : {isbn} is already exist in system.")
        {
        }
    }
}

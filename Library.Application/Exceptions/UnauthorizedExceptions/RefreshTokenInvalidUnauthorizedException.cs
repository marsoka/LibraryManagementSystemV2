using Library.Application.Execptions.StatusCodesExeptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.Exceptions.UnauthorizedExceptions
{
    public class RefreshTokenInvalidUnauthorizedException : UnauthorizedException
    {
        public RefreshTokenInvalidUnauthorizedException(int id)
            : base($"Refresh Token with id {id} Invalid.")
        {
        }

        public RefreshTokenInvalidUnauthorizedException()
            : base($"Refresh Token Invalid.")
        {
        }
    }
}

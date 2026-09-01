using Library.Application.Execptions.StatusCodesExeptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.Exceptions.UnauthorizedExceptions
{
    public class RefreshTokenExpiredUnauthorizedException : UnauthorizedException
    {
        public RefreshTokenExpiredUnauthorizedException()
            : base($"The Refresh Token Expired.")
        {
        }
    }
}

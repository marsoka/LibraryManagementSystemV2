using Library.Application.Execptions.StatusCodesExeptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.Exceptions.UnauthorizedExceptions
{
    public class RefreshTokenIsRevokedUnauthorizedException : UnauthorizedException
    {
        public RefreshTokenIsRevokedUnauthorizedException()
            : base($"The Refresh Token is revoked.")
        {
        }
    }
}

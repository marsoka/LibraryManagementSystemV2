

using Library.Application.Exceptions;
using System.Net;

namespace Library.Application.Execptions.StatusCodesExeptions
{
    public class UnauthorizedException : AppException
    {
        public UnauthorizedException(string message)
            : base(message, ((int)HttpStatusCode.Unauthorized))
        {
        }

        public UnauthorizedException() 
            : base("The user name or password is invaild.", ((int)HttpStatusCode.Unauthorized)) 
        {
        }
    }
}


using System.Net;

namespace Library.Application.Execptions.StatusCodesExeptions
{
    public class UnauthorizedException : AppException
    {
        public UnauthorizedException(string message = "Unauthorized.")
            : base(message, ((int)HttpStatusCode.Unauthorized))
        {
        }
    }
}
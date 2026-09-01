

using System.Net;

namespace Library.Application.Execptions.StatusCodesExeptions
{
    public class ForbiddenException : AppException
    {
        public ForbiddenException(string message = "Access denied.")
            : base(message, ((int)HttpStatusCode.Forbidden))
        {
        }
    }
}


using System.Net;

namespace Library.Application.Execptions.StatusCodesExeptions
{
    public class ConflictException : AppException
    {
        public ConflictException(string message)
            : base(message, ((int)HttpStatusCode.Conflict))
        {
        }
    }
}
using Library.Application.Exceptions;
using System.Net;

namespace Library.Application.Execptions.StatusCodesExeptions
{
    public class BadRequestException : AppException
    {
        public BadRequestException(string message)
            : base(message, ((int)HttpStatusCode.BadRequest))
        {
        }
    }
}
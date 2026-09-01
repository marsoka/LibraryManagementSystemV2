using Library.Application.Exceptions;
using System.Net;

namespace Library.Application.Execptions.StatusCodesExeptions
{
    public class NotFoundException : AppException
    {
        public NotFoundException(string message)
            : base(message, ((int)HttpStatusCode.NotFound))
        {
        }
    }
}
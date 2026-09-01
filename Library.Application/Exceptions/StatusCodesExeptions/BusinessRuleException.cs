
using System.Net;

namespace Library.Application.Execptions.StatusCodesExeptions
{
    public class BusinessRuleException : AppException
    {
        public BusinessRuleException(string message)
            : base(message, ((int)HttpStatusCode.UnprocessableEntity))
        {
        }
    }
}
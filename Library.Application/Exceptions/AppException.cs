using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Library.Application.Execptions
{
    public class AppException : Exception
    {
        public int StatusCode { get;}
        public AppException(string message, int statusCode)
            : base(message)
        {
            StatusCode = statusCode;
        }
    }
}

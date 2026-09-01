using Library.Application.Execptions.StatusCodesExeptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.Exceptions.NotFoundExceptions
{
    public class PublisherNotFoundException : NotFoundException
    {
        public PublisherNotFoundException(int id)
            : base($"Publisher with id {id} not found.")
        {
        }
    }
}

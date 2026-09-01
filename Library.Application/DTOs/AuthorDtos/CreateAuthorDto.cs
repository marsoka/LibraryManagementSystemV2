using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.DTOs.AuthorDtos
{
    public class CreateAuthorDto
    {
        public required string FullName { get; set; }
        public required string Biography { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public required string Nationality { get; set; }
    }
}

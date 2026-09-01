using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.DTOs.PublisherDtos
{
    public class CreatePublisherDto
    {
        public required string Name { get; set; }
        public required string Address { get; set; }
        public required string Phone { get; set; }
    }
}

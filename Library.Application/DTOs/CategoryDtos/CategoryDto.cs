using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.DTOs.CategoryDtos
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}

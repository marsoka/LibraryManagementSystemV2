using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.DTOs.CategoryDtos
{
    public class UpdateCategoryDto
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}

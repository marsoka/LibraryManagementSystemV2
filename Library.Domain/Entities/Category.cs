using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }

        public List<Book>? Books { get; set; }
    }
}

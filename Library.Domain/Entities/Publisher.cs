using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain.Entities
{
    public class Publisher
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }
        public required string Phone { get; set; }

        public List<Book>? Books { get; set; }
    }
}

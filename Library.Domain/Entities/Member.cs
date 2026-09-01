using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain.Entities
{
    public class Member
    {
        public int Id { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required string Address { get; set; }
        public DateOnly RegistrationDate { get; set; }

        public List<Borrowing>? Borrowings { get; set; }
    }
}

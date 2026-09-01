using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.DTOs.MemberDtos
{
    public class CreateMemberDto
    {
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required string Address { get; set; }
    }
}

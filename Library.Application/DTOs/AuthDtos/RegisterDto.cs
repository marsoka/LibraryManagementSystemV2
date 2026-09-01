using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.DTOs.AuthDtos
{
    public class RegisterDto
    {
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string ConfirmPassword { get; set; }
    }
}

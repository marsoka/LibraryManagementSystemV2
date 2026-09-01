using Library.Domain.Entities.auth;
using Library.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public required UserRole Role { get; set; }

        public List<RefreshToken>? RefreshTokens { get; set; }
    }
}

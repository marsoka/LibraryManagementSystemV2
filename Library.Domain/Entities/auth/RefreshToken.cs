using Library.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain.Entities.auth
{
    public class RefreshToken
    {
        public int Id { get; set; }

        public required string TokenHash { get; set; }

        public DateTime ExpiresAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? RevokedAt { get; set; }

        public bool IsRevoked { get; set; }

        public RefreshTokenRevokedReason? RevokedReason { get; set; }


        public int UserId { get; set; }

        public User User { get; set; } = null!;
    }
}

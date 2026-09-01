using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.DTOs.AuthDtos
{
    public class AuthResponseDto
    {
        public required string AccessToken { get; set; }

        public required string RefreshToken { get; set; }

        public DateTime AccessTokenExpiresAt { get; set; }

    }
}

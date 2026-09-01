using Library.Application.DTOs.AuthDtos;
using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Library.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> Login(LoginDto loginDto);
        Task RegiserUser(RegisterDto registerDto);
        Task Logout(ClaimsPrincipal user, RefreshTokenDto refreshTokenDto);
        Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenDto refreshTokenDto);

    }
}

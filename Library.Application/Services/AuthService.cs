using AutoMapper;
using FluentValidation;
using Library.Application.Abstractions.Repositories;
using Library.Application.DTOs.AuthDtos;
using Library.Application.Exceptions.ExistExceptions;
using Library.Application.Exceptions.UnauthorizedExceptions;
using Library.Application.Interfaces;
using Library.Domain;
using Library.Domain.Entities;
using Library.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using BCrypt.Net;
using Library.Application.Execptions.StatusCodesExeptions;
using Library.Domain.Entities.auth;
using System.Security.Claims;
using Microsoft.Extensions.Options;



namespace Library.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly JwtSettings _jwtSettings;
        private readonly ITokenService _tokenService;
        private readonly IValidator<RegisterDto> _validatorRegister;
        private readonly IMapper _mapper;

        public AuthService(IUnitOfWork unitOfWork,
            IOptions<JwtSettings> jwtSettings,
            ITokenService tokenService, 
            IValidator<RegisterDto> validatorRegister, 
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _jwtSettings = jwtSettings.Value;
            _tokenService = tokenService;
            _validatorRegister = validatorRegister;
            _mapper = mapper;
        }

        public async Task<AuthResponseDto> Login(LoginDto loginDto)
        {
            var user = await _unitOfWork.User
                .FindAsync(u => u.Email == loginDto.Email);

            if (user is null)
                throw new UnauthorizedException();

            if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
                throw new UnauthorizedException();

            var accessToken = _tokenService.GenerateAccessToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();
            var refreshHash = _tokenService.HashRefreshToken(refreshToken);

            var time = DateTime.UtcNow;

            var refresh = new RefreshToken
            {
                TokenHash = refreshHash,
                CreatedAt = time,
                ExpiresAt = time.AddDays(7),
                UserId = user.Id,
                IsRevoked = false
            };

            await _unitOfWork.RefreshToken.AddAsync(refresh);
            await _unitOfWork.CompleteAsync();

            return new AuthResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                AccessTokenExpiresAt = time.AddMinutes(_jwtSettings.ExpireMinutes)
            };
        }

        public async Task Logout(ClaimsPrincipal user, RefreshTokenDto refreshTokenDto)
        {
            var refresh = await _unitOfWork.RefreshToken
                .FindAsync(rt =>
                rt.TokenHash == _tokenService.HashRefreshToken(refreshTokenDto.RefreshToken));

            if (refresh == null)
                throw new RefreshTokenInvalidUnauthorizedException();

            if (refresh.ExpiresAt < DateTime.UtcNow)
                throw new RefreshTokenExpiredUnauthorizedException();

            if (refresh.IsRevoked)
                throw new RefreshTokenIsRevokedUnauthorizedException();


            var userId = int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            if (refresh.UserId != userId)
                throw new UnauthorizedException();


            refresh.IsRevoked = true;

            refresh.RevokedAt = DateTime.UtcNow;

            refresh.RevokedReason = RefreshTokenRevokedReason.Logout;

            _unitOfWork.RefreshToken.Update(refresh);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenDto refreshTokenDto)
        {
            var refreshTokenDB = await _unitOfWork.RefreshToken
                .FindAsync(r => r.TokenHash == _tokenService.HashRefreshToken(refreshTokenDto.RefreshToken), ["User"]);

            if (refreshTokenDB is null)
                throw new RefreshTokenInvalidUnauthorizedException();

            if (refreshTokenDB.ExpiresAt < DateTime.UtcNow)
                throw new RefreshTokenExpiredUnauthorizedException();

            if (refreshTokenDB.IsRevoked)
                throw new RefreshTokenIsRevokedUnauthorizedException();

            var user = refreshTokenDB.User;

            var newaccessToken = _tokenService.GenerateAccessToken(user);
            var newRefreshToken = _tokenService.GenerateRefreshToken();
            var newRefreshHash = _tokenService.HashRefreshToken(newRefreshToken);
            var time = DateTime.UtcNow;

            refreshTokenDB.IsRevoked = true;
            refreshTokenDB.RevokedAt = time;
            refreshTokenDB.RevokedReason = RefreshTokenRevokedReason.Rotation;
            _unitOfWork.RefreshToken.Update(refreshTokenDB);

            await _unitOfWork.RefreshToken.AddAsync(
                new RefreshToken
                {
                    TokenHash = newRefreshHash,
                    CreatedAt = time,
                    ExpiresAt = time.AddDays(7),
                    IsRevoked = false,
                    UserId = user.Id
                }
            );

            await _unitOfWork.CompleteAsync();

            return new AuthResponseDto
            {
                AccessToken = newaccessToken,
                RefreshToken = newRefreshToken,
                AccessTokenExpiresAt = time.AddMinutes(_jwtSettings.ExpireMinutes)
            };
        }

        public async Task RegiserUser(RegisterDto registerDto)
        {
            await _validatorRegister.ValidateAndThrowAsync(registerDto);

            if (await _unitOfWork.User
                .AnyAsync(u => u.Username == registerDto.Username))
                throw new UsernameExistException(registerDto.Username);

            if (await _unitOfWork.User
                .AnyAsync(u => u.Email == registerDto.Email))
                throw new EmailExistException(registerDto.Email);


            var user = _mapper.Map<User>(registerDto);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);
            user.Role = UserRole.Admin;

            await _unitOfWork.User.AddAsync(user);
            await _unitOfWork.CompleteAsync();

        }
    }
}

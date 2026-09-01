using Library.Application.DTOs.AuthDtos;
using Library.Application.DTOs.AuthorDtos;
using Library.Application.Interfaces;
using Library.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            await _service.RegiserUser(registerDto);
            return Created();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            return Ok(await _service.Login(loginDto));
        }


        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken(RefreshTokenDto refreshTokenDto)
        {
            return Ok(await _service.RefreshTokenAsync(refreshTokenDto));
        }

        [Authorize]
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout(RefreshTokenDto refreshTokenDto)
        {
            await _service.Logout(User, refreshTokenDto);
            return Ok();
        }

    }
}

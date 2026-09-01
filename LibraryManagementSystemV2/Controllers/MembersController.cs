using Library.Application.DTOs.BookDtos;
using Library.Application.DTOs.MemberDtos;
using Library.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,Librarian")]
    public class MembersController : Controller
    {
        private readonly IMemberService _service;

        public MembersController(IMemberService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetMembersAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _service.GetMemberByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateMember(CreateMemberDto createMemberDto)
        {
            await _service.CreateMemberAsync(createMemberDto);

            return Created();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMember(int id, UpdateMemberDto updateMemberDto)
        {
            await _service.UpdateMemberAsync(id, updateMemberDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(int id)
        {
            await _service.DeleteMemberAsync(id);
            return NoContent();
        }
    }
}

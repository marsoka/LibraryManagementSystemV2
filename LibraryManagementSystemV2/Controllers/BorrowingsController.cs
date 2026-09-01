using Library.Application.DTOs.BorrowingDtos;
using Library.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,Librarian")]
    public class BorrowingsController : ControllerBase
    {
        private readonly IBorrowingServices _services;

        public BorrowingsController(IBorrowingServices services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]BorrowingQueryDto query)
        {
            return Ok(await _services.GetBorrowingsAsync(query));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok( await _services.GetBorrowingByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateBorrowing(CreateBorrowingDto createBorrowingDto)
        {
            await _services.CreateBorrowingAsync(createBorrowingDto);

            return Created();
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateBorrowing(int id, UpdateBorrowingDto updateBorrowingDto)
        //{
        //    await _services.UpdateBorrowingAsync(id, updateBorrowingDto);
        //    return NoContent();
        //}

        [HttpPost("{borrwoingId}/return")]
        public async Task<IActionResult> ReturnBook(int borrwoingId)
        {
            await _services.ReturnBookAsync(borrwoingId);
            return NoContent();
        }

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteBorrowing(int id)
        //{
        //    await _services.DeleteBorrowingAsync(id);
        //    return Ok();
        //}
    }
}

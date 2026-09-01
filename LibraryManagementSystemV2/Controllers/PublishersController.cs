using Library.Application.DTOs.PublisherDtos;
using Library.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Librarian")]
    public class PublishersController : ControllerBase
    {
        private readonly IPublisherService _publisherService;

        public PublishersController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var publishers = await _publisherService.GetPublishersAsync();
            return Ok(publishers);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var publisher = await _publisherService.GetPublisherByIdAsync(id);
            return Ok(publisher);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePublisherDto dto)
        {
            await _publisherService.CreatePublisherAsync(dto);
            return Created();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdatePublisherDto dto)
        {
            await _publisherService.UpdatePublisherAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _publisherService.DeletePublisherAsync(id);
            return NoContent();
        }
    }
}
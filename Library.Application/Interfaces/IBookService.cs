using Library.Application.DTOs.BookDtos;
using Library.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.Interfaces
{
    public interface IBookService
    {
        Task<PaginatedResult<BookDto>> GetBooksAsync(BookQueryDto query);
        Task<BookDto?> GetBookByIdAsync(int id);
        Task CreateBookAsync(CreateBookDto dto);
        Task UpdateBookAsync(int id, UpdateBookDto dto);
        Task DeleteBookAsync(int id);

    }
}

using Library.Application.DTOs.BookDtos;
using Library.Domain.Entities;
using Library.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.Abstractions.Repositories
{
    public interface IBookRepository : IBaseRepository<Book>
    {
        Task<PaginatedResult<Book>> GetPaginatedResultAsync(BookQueryDto bookQueryDto);
    }
}

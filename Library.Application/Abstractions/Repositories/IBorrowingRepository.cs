using Library.Application.DTOs.BorrowingDtos;
using Library.Domain.Entities;
using Library.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.Abstractions.Repositories
{
    public interface IBorrowingRepository : IBaseRepository<Borrowing>
    {
        Task<PaginatedResult<Borrowing>> PaginatedResultAsync(BorrowingQueryDto query);
    }
}

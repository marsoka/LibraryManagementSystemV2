using Library.Application.DTOs.BorrowingDtos;
using Library.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.Interfaces
{
    public interface IBorrowingServices
    {
        Task<PaginatedResult<BorrowingDto>> GetBorrowingsAsync(BorrowingQueryDto query);
        Task<BorrowingDto?> GetBorrowingByIdAsync(int id);
        Task CreateBorrowingAsync(CreateBorrowingDto dto);
        Task ReturnBookAsync(int borrowingId);
        //Task UpdateBorrowingAsync(int id, UpdateBorrowingDto dto);
        //Task DeleteBorrowingAsync(int id);
    }
}

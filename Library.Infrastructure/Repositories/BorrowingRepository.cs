using Library.Application.Abstractions.Repositories;
using Library.Application.DTOs.BorrowingDtos;
using Library.Domain.Entities;
using Library.Domain.Enums;
using Library.Domain.Responses;
using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure.Repositories
{
    public class BorrowingRepository : BaseRepository<Borrowing>, IBorrowingRepository
    {
        public BorrowingRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<PaginatedResult<Borrowing>> PaginatedResultAsync(BorrowingQueryDto query)
        {
            IQueryable<Borrowing> borrowings = _context.Borrowings;

            if (query.MemberId.HasValue)
                borrowings = borrowings.Where(b =>
                    b.MemberId == query.MemberId);

            if(query.BookId.HasValue)
                borrowings = borrowings.Where(b =>
                    b.BookId == query.BookId);

            if(query.MinBorrowDate.HasValue)
                borrowings = borrowings.Where(b => 
                    b.BorrowDate >= query.MinBorrowDate);

            if (query.MaxBorrowDate.HasValue)
                borrowings = borrowings.Where(b =>
                    b.BorrowDate <= query.MaxBorrowDate);

            if(query.MinDueDate.HasValue)
                borrowings = borrowings.Where(b => 
                    b.DueDate >= query.MinDueDate);

            if (query.MaxDueDate.HasValue)
                borrowings = borrowings.Where(b =>
                    b.DueDate <= query.MaxDueDate);

            if(query.MinReturnDate.HasValue)
                borrowings = borrowings.Where(b =>
                    b.ReturnDate >= query.MinReturnDate);

            if (query.MaxReturnDate.HasValue)
                borrowings = borrowings.Where(b =>
                    b.ReturnDate <= query.MaxReturnDate);

            if (query.Status.HasValue)
            {
                borrowings = (int)query.Status switch
                {
                    (int)BorrowingStatus.Borrowed => borrowings
                        .Where(b =>
                            b.Status == BorrowingStatus.Borrowed),

                    (int)BorrowingStatus.Returned => borrowings
                        .Where(b =>
                            b.Status == BorrowingStatus.Returned),

                    (int)BorrowingStatus.Overdue => borrowings
                        .Where(b =>
                            b.Status == BorrowingStatus.Overdue),

                    _ => throw new NotImplementedException(),
                };
            }

            
            borrowings = (int?)query.SortBy switch
            {
                    (int)BorrowingSortBy.MemberId => query.Descending
                        ? borrowings.OrderByDescending(b => b.MemberId)
                        : borrowings.OrderBy(b => b.MemberId),

                    (int)BorrowingSortBy.BookId => query.Descending
                        ? borrowings.OrderByDescending(b => b.BookId)
                        : borrowings.OrderBy(b => b.BookId),


                    (int)BorrowingSortBy.BorrowDate => query.Descending
                        ? borrowings.OrderByDescending(b => b.BorrowDate)
                        : borrowings.OrderBy(b => b.BorrowDate),

                    (int)BorrowingSortBy.DueDate => query.Descending
                        ? borrowings.OrderByDescending(b => b.DueDate)
                        : borrowings.OrderBy(b => b.DueDate),

                    (int)BorrowingSortBy.ReturnDate => query.Descending
                        ? borrowings.OrderByDescending(b => b.ReturnDate)
                        : borrowings.OrderBy(b => b.ReturnDate),

                    _ => query.Descending
                        ? borrowings.OrderByDescending(b => b.Id)
                        : borrowings.OrderBy(b => b.Id)
            };
            

            var totalCount = await borrowings.CountAsync();

            var items = await borrowings
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync();

            return new PaginatedResult<Borrowing>
            {
                Items = items,
                PageNumber = query.PageNumber,
                PageSize = query.PageSize,
                TotalCount = totalCount,
            };
        }
    }
}

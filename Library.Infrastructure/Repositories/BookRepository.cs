using Library.Application.Abstractions.Repositories;
using Library.Application.DTOs.BookDtos;
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
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<PaginatedResult<Book>> GetPaginatedResultAsync(BookQueryDto query)
        {
            IQueryable<Book> books = _context.Books;

            if(!string.IsNullOrEmpty(query.Search))
            {
                string search = query.Search.Trim();
                books = books.Where(b =>
                    b.Title.Contains(search) ||
                    b.ISBN.Contains(search));
            }

            if (query.AuthorId.HasValue)
            {
                books = books.Where(b =>
                    b.AuthorId ==  query.AuthorId.Value);
            }

            if (query.CategoryId.HasValue)
            {
                books = books.Where(b =>
                    b.CategoryId == query.CategoryId.Value);
            }

            if (query.PublisherId.HasValue)
            {
                books = books.Where(b =>
                    b.PublisherId == query.PublisherId.Value);
            }

            if (query.MaxYear.HasValue)
            {
                books = books.Where(b => 
                    b.PublicationYear <= query.MaxYear.Value);
            }

            if (query.MinYear.HasValue)
            {
                books = books.Where(b =>
                    b.PublicationYear >= query.MinYear.Value);
            }

            if (query.MaxPrice.HasValue)
            {
                books = books.Where(b =>
                    b.Price <= query.MaxPrice.Value);
            }

            if (query.MinPrice.HasValue)
            {
                books = books.Where(b =>
                    b.Price >= query.MinPrice.Value);
            }

            if (query.AvailableOnly == true)
            {
                books = books.Where(b =>
                    b.AvailableCopies > 0);
            }

            
            books = (int?)query.SortBy switch
            {
                    (int)BookSortBy.Title => query.Descending
                        ? books.OrderByDescending(b => b.Title)
                        : books.OrderBy(b => b.Title),

                    (int)BookSortBy.Price => query.Descending
                        ? books.OrderByDescending(b => b.Price)
                        : books.OrderBy(b => b.Price),

                    (int)BookSortBy.PublicationYear => query.Descending
                        ? books.OrderByDescending(b => b.PublicationYear)
                        : books.OrderBy(b => b.PublicationYear),

                    (int)BookSortBy.AvailableCopies => query.Descending
                        ? books.OrderByDescending(b => b.AvailableCopies)
                        : books.OrderBy(b => b.AvailableCopies),

                    _ => query.Descending
                        ? books.OrderByDescending(b => b.Id)
                        : books.OrderBy(b => b.Id)
            };
            

            var totalCount = await books.CountAsync();

            var items = await books
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync();

            return new PaginatedResult<Book>
            {
                Items = items,
                PageNumber = query.PageNumber,
                PageSize = query.PageSize,
                TotalCount = totalCount
            };
        }
    }
}

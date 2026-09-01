using AutoMapper.Execution;
using Library.Application.Abstractions.Repositories;
using Library.Domain.Entities;
using Library.Domain.Entities.auth;
using Library.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Member = Library.Domain.Entities.Member;

namespace Library.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IBaseRepository<Author> Authors {  get; private set; }
        public IBaseRepository<Category> Categories { get; private set; }
        public IBaseRepository<Publisher> Publishers { get; private set; }
        public IBookRepository Books { get; private set; }
        public IBaseRepository<Member> Members { get; private set; }
        public IBorrowingRepository Borrowings { get; private set; }

        public IBaseRepository<User> User { get; private set; }

        public IBaseRepository<RefreshToken> RefreshToken { get; private set; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;

            Authors = new BaseRepository<Author>(_context);
            Categories = new BaseRepository<Category>(_context);
            Publishers = new BaseRepository<Publisher>(_context);
            Books = new BookRepository(_context);
            Members = new BaseRepository<Member>(_context);
            Borrowings = new BorrowingRepository(_context);
            User = new BaseRepository<User>(_context);
            RefreshToken = new BaseRepository<RefreshToken>(_context);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

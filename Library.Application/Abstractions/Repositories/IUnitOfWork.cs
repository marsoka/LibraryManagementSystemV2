using Library.Domain.Entities;
using Library.Domain.Entities.auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.Abstractions.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<Author> Authors { get;}
        IBaseRepository<Category> Categories { get; }
        IBaseRepository<Publisher> Publishers { get;  }
        IBookRepository Books { get; }
        IBaseRepository<Member> Members { get; }
        IBorrowingRepository Borrowings { get; }
        IBaseRepository<User> User { get; }
        IBaseRepository<RefreshToken> RefreshToken { get; }
        Task<int> CompleteAsync();
    }
}

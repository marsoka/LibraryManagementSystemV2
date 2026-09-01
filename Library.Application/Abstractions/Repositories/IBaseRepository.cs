using Library.Application.DTOs.BookDtos;
using Library.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Library.Application.Abstractions.Repositories
{
    public interface IBaseRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<bool> AnyAsync(System.Linq.Expressions.Expression<Func<T, bool>> expression);
        Task<T?> FindAsync(Expression<Func<T, bool>> expression, string[] includes = null);

    }
}

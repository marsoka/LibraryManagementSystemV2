using Library.Application.DTOs.AuthorDtos;
using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorDto>> GetAuthorsAsync();
        Task<AuthorDto?> GetAuthorByIdAsync(int id);
        Task CreateAuthorAsync(CreateAuthorDto dto);
        Task UpdateAuthorAsync(int id, UpdateAuthorDto dto);
        Task DeleteAuthorAsync(int id);
    }
}

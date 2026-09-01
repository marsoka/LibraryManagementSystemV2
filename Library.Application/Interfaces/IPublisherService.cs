using Library.Application.DTOs.AuthorDtos;
using Library.Application.DTOs.PublisherDtos;
using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.Interfaces
{
    public interface IPublisherService
    {
        Task<IEnumerable<PublisherDto>> GetPublishersAsync();
        Task<PublisherDto?> GetPublisherByIdAsync(int id);
        Task CreatePublisherAsync(CreatePublisherDto dto);
        Task UpdatePublisherAsync(int id, UpdatePublisherDto dto);
        Task DeletePublisherAsync(int id);
    }
}

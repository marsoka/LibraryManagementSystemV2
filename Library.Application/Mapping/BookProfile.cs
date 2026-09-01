using AutoMapper;
using Library.Application.DTOs.BookDtos;
using Library.Domain.Entities;
using Library.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.Mapping
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<CreateBookDto, Book>();

            CreateMap<UpdateBookDto, Book>()
                .ForMember(
                    dest => dest.TotalCopies,
                    opt => opt.Ignore());

            CreateMap<Book, BookDto>();

            CreateMap<PaginatedResult<Book>, PaginatedResult<BookDto>>();
        }
    }
}

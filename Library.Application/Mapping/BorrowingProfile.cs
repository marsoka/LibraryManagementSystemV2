using AutoMapper;
using Library.Application.DTOs.BorrowingDtos;
using Library.Domain.Entities;
using Library.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.Mapping
{
    public class BorrowingProfile : Profile
    {
        public BorrowingProfile()
        {
            CreateMap<CreateBorrowingDto, Borrowing>();
            CreateMap<UpdateBorrowingDto, Borrowing>();
            CreateMap<Borrowing, BorrowingDto>();
            CreateMap<PaginatedResult<Borrowing>, PaginatedResult<BorrowingDto>>();
        }
    }
}

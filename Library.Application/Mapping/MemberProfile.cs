using AutoMapper;
using Library.Application.DTOs;
using Library.Application.DTOs.MemberDtos;
using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.Mapping
{
    public class MemberProfile : Profile
    {
        public MemberProfile()
        {
            CreateMap<CreateMemberDto, Member>();
            CreateMap<UpdateMemberDto, Member>();
            CreateMap<Member, MemberDto>();
        }
    }
}

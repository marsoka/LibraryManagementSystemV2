using Library.Application.DTOs.BookDtos;
using Library.Application.DTOs.MemberDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.Interfaces
{
    public interface IMemberService
    {
        Task<IEnumerable<MemberDto>> GetMembersAsync();
        Task<MemberDto?> GetMemberByIdAsync(int id);
        Task CreateMemberAsync(CreateMemberDto dto);
        Task UpdateMemberAsync(int id, UpdateMemberDto dto);
        Task DeleteMemberAsync(int id);
    }
}

using AutoMapper;
using FluentValidation;
using Library.Application.Abstractions.Repositories;
using Library.Application.DTOs.MemberDtos;
using Library.Application.Exceptions.NotFoundExceptions;
using Library.Application.Exceptions.BusinessRuleExceptions;
using Library.Application.Interfaces;
using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Library.Application.Exceptions.ExistExceptions;

namespace Library.Application.Services
{
    public class MemberServices : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateMemberDto> _validatorCreate;
        private readonly IValidator<UpdateMemberDto> _validatorUpdate;

        public MemberServices(IUnitOfWork unitOfWork, IMapper mapper,
            IValidator<CreateMemberDto> validatorCreate,
            IValidator<UpdateMemberDto> validatorUpdate)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validatorCreate = validatorCreate;
            _validatorUpdate = validatorUpdate;
        }

        public async Task CreateMemberAsync(CreateMemberDto dto)
        {
            await _validatorCreate.ValidateAndThrowAsync(dto);

            if (await _unitOfWork.Members.AnyAsync(m => m.Email == dto.Email))
                throw new EmailExistException(dto.Email);

            if (await _unitOfWork.Members.AnyAsync(m => m.Phone == dto.Phone))
                throw new PhoneExistException(dto.Phone);

            var member = _mapper.Map<Member>(dto);
            member.RegistrationDate = DateOnly.FromDateTime(DateTime.UtcNow);

            await _unitOfWork.Members.AddAsync(member);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteMemberAsync(int id)
        {
            var member = await _unitOfWork.Members.GetByIdAsync(id);
            if (member is null)
                throw new MemberNotFoundException(id);

            if (await _unitOfWork.Borrowings.AnyAsync(br =>
                br.MemberId == id))
                throw new MemberHasHistoryException(id);

            _unitOfWork.Members.Delete(member);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<MemberDto?> GetMemberByIdAsync(int id)
        {
            var member = await _unitOfWork.Members.GetByIdAsync(id);
            if (member is null)
                throw new MemberNotFoundException(id);

            return _mapper.Map<MemberDto>(member);
        }

        public async Task<IEnumerable<MemberDto>> GetMembersAsync()
        {
            var list = await _unitOfWork.Members.GetAllAsync();
            return _mapper.Map<IEnumerable<MemberDto>>(list);
        }

        public async Task UpdateMemberAsync(int id, UpdateMemberDto dto)
        {
            var member = await _unitOfWork.Members.GetByIdAsync(id);
            if (member is null)
                throw new MemberNotFoundException(id);

            if (await _unitOfWork.Members.AnyAsync(m => m.Email == dto.Email && m.Id != id))
                throw new EmailExistException(dto.Email);

            if (await _unitOfWork.Members.AnyAsync(m => m.Phone == dto.Phone && m.Id != id))
                throw new PhoneExistException(dto.Phone);

            _mapper.Map(dto, member);

            _unitOfWork.Members.Update(member);
            await _unitOfWork.CompleteAsync();
        }
    }
}

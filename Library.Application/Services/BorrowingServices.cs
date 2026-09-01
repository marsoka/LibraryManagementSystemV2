using AutoMapper;
using FluentValidation;
using Library.Application.Abstractions.Repositories;
using Library.Application.DTOs.BorrowingDtos;
using Library.Application.Exceptions.BusinessRuleExceptions;
using Library.Application.Exceptions.DuplicateExceptions;
using Library.Application.Exceptions.NotFoundExceptions;
using Library.Application.Interfaces;
using Library.Domain.Entities;
using Library.Domain.Enums;
using Library.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.Services
{
    public class BorrowingServices : IBorrowingServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateBorrowingDto> _validatorCreate;
        private readonly IValidator<UpdateBorrowingDto> _validatorUpdate;
        private readonly IValidator<BorrowingQueryDto> _validatorQuery;

        public BorrowingServices(IUnitOfWork unitOfWork, IMapper mapper,
            IValidator<CreateBorrowingDto> validatorCreate, 
            IValidator<UpdateBorrowingDto> validatorUpdate,
            IValidator<BorrowingQueryDto> validatorQuery)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validatorCreate = validatorCreate;
            _validatorUpdate = validatorUpdate;
            _validatorQuery = validatorQuery;
        }

        public async Task CreateBorrowingAsync(CreateBorrowingDto dto)
        {
            await _validatorCreate.ValidateAndThrowAsync(dto);

            var book = await _unitOfWork.Books.GetByIdAsync(dto.BookId);
            if (book is null)
                throw new BookNotFoundException(dto.BookId);
            var member = await _unitOfWork.Members.GetByIdAsync(dto.MemberId);
            if (member is null)
                throw new MemberNotFoundException(dto.MemberId);

            var IsExist = await _unitOfWork.Borrowings
                .AnyAsync(b => b.BookId == dto.BookId
                    && b.MemberId == dto.MemberId
                    && b.ReturnDate == null);
            if (IsExist)
                throw new DuplicateBorrowingException(dto.MemberId, dto.BookId);

            book.Borrow();

            var borrowing = _mapper.Map<Borrowing>(dto);
            var time = DateTime.UtcNow;
            borrowing.BorrowDate = time;
            borrowing.DueDate = time.AddDays(14);
            borrowing.Status = BorrowingStatus.Borrowed;
            borrowing.ReturnDate = null;

            await _unitOfWork.Borrowings.AddAsync(borrowing);
            _unitOfWork.Books.Update(book);
            await _unitOfWork.CompleteAsync();
        }

        //public async Task DeleteBorrowingAsync(int id)
        //{
        //    var borrowing = await _unitOfWork.Borrowings.GetByIdAsync(id);
        //    if (borrowing is null)
        //        throw new BorrowingNotFoundException(id);

        //    _unitOfWork.Borrowings.Delete(borrowing);
        //    await _unitOfWork.CompleteAsync();
        //}

        public async Task<BorrowingDto?> GetBorrowingByIdAsync(int id)
        {
            var borrowing = await _unitOfWork.Borrowings.GetByIdAsync(id);
            if (borrowing is null)
                throw new BorrowingNotFoundException(id);

            return _mapper.Map<BorrowingDto>(borrowing);
        }

        public async Task<PaginatedResult<BorrowingDto>> GetBorrowingsAsync(BorrowingQueryDto query)
        {
            await _validatorQuery.ValidateAndThrowAsync(query);

            var list = await _unitOfWork.Borrowings.PaginatedResultAsync(query);
            return _mapper.Map<PaginatedResult<BorrowingDto>>(list);
        }

        public async Task ReturnBookAsync(int borrowingId)
        {
            var borrowing = await _unitOfWork.Borrowings.GetByIdAsync(borrowingId);
            if (borrowing is null)
                throw new BorrowingNotFoundException(borrowingId);

            var book = await _unitOfWork.Books.GetByIdAsync(borrowing.BookId);
            if (book is null)
                throw new BookNotFoundException(borrowing.BookId);

            if (borrowing.Status == BorrowingStatus.Returned 
                || borrowing.ReturnDate != null)
                throw new BorrowingIsReturedException(borrowingId);

            book.Return();
            borrowing.Return(DateTime.UtcNow);


            _unitOfWork.Borrowings.Update(borrowing);
            _unitOfWork.Books.Update(book);

            await _unitOfWork.CompleteAsync();
        }

        //public async Task UpdateBorrowingAsync(int id, UpdateBorrowingDto dto)
        //{
        //    var borrowing = await _unitOfWork.Borrowings.GetByIdAsync(id);
        //    if (borrowing is null)
        //        throw new BorrowingNotFoundException(id);

        //    var book = await _unitOfWork.Books.GetByIdAsync(dto.BookId);
        //    if (book is null)
        //        throw new BookNotFoundException(dto.BookId);
        //    var member = await _unitOfWork.Members.GetByIdAsync(dto.MemberId);
        //    if (member is null)
        //        throw new MemberNotFoundException(dto.MemberId);

        //    var IsExist = await _unitOfWork.Borrowings
        //        .AnyAsync(b => b.BookId == dto.BookId 
        //            && b.MemberId == dto.MemberId 
        //            && b.Status != BorrowingStatus.Returned);
        //    if (IsExist)
        //        throw new DuplicateBorrowingException(dto.MemberId, dto.BookId);

        //    _mapper.Map(dto, borrowing);

        //    _unitOfWork.Borrowings.Update(borrowing);
        //    await _unitOfWork.CompleteAsync();
        //}
    }
}

using AutoMapper;
using FluentValidation;
using Library.Application.Abstractions.Repositories;
using Library.Application.DTOs.BookDtos;
using Library.Application.Exceptions.BusinessRuleExceptions;
using Library.Application.Exceptions.ExistExceptions;
using Library.Application.Exceptions.NotFoundExceptions;
using Library.Application.Interfaces;
using Library.Domain.Entities;
using Library.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.Services
{
    public class BookServices : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateBookDto> _validatorCreate;
        private readonly IValidator<UpdateBookDto> _validateUpdate;
        private readonly IValidator<BookQueryDto> _validateBookQuery;

        public BookServices(IUnitOfWork unitOfWork, IMapper mapper,
            IValidator<CreateBookDto> validatorCreate,
            IValidator<UpdateBookDto> validateUpdate,
            IValidator<BookQueryDto> validateBookQuery)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validatorCreate = validatorCreate;
            _validateUpdate = validateUpdate;
            _validateBookQuery = validateBookQuery;
        }

        public async Task CreateBookAsync(CreateBookDto dto)
        {
            await _validatorCreate.ValidateAndThrowAsync(dto);

            var author = await _unitOfWork.Authors.GetByIdAsync(dto.AuthorId);
            if (author is null)
                throw new AuthorNotFoundException(dto.AuthorId);

            var category = await _unitOfWork.Categories.GetByIdAsync(dto.CategoryId);
            if(category is null)
                throw new CategoryNotFoundException(dto.CategoryId);

            var publisher = await _unitOfWork.Publishers.GetByIdAsync(dto.PublisherId);
            if (publisher is null)
                throw new PublisherNotFoundException(dto.PublisherId);


            if (await _unitOfWork.Books.AnyAsync(b => b.ISBN == dto.ISBN))
                throw new IsbnExistException(dto.ISBN);

            var book = _mapper.Map<Book>(dto);
            book.SetTotalAndAvailableCopies(dto.TotalCopies);

            await _unitOfWork.Books.AddAsync(book);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(id);
            if (book == null)
                throw new BookNotFoundException(id);

            if (await _unitOfWork.Borrowings.AnyAsync(br =>
                br.BookId == id))
                throw new BookHasHistoryException(id);

            _unitOfWork.Books.Delete(book);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<BookDto?> GetBookByIdAsync(int id)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(id);

            if (book == null)
                throw new BookNotFoundException(id);

            return _mapper.Map<BookDto>(book);
        }

        public async Task<PaginatedResult<BookDto>> GetBooksAsync(BookQueryDto query)
        {
            await _validateBookQuery.ValidateAndThrowAsync(query);

            var list = await _unitOfWork.Books.GetPaginatedResultAsync(query);
            return _mapper.Map<PaginatedResult<BookDto>>(list);
        }

        public async Task UpdateBookAsync(int id, UpdateBookDto dto)
        {
            await _validateUpdate.ValidateAndThrowAsync(dto);

            var book = await _unitOfWork.Books.GetByIdAsync(id);

            if (book is null)
                throw new BookNotFoundException(id);
            
            var author = await _unitOfWork.Authors.GetByIdAsync(dto.AuthorId);
            if (author is null)
                throw new AuthorNotFoundException(dto.AuthorId);

            var category = await _unitOfWork.Categories.GetByIdAsync(dto.CategoryId);
            if (category is null)
                throw new CategoryNotFoundException(dto.CategoryId);

            var publisher = await _unitOfWork.Publishers.GetByIdAsync(dto.PublisherId);
            if (publisher is null)
                throw new PublisherNotFoundException(dto.PublisherId);


            if (await _unitOfWork.Books.AnyAsync(b => b.ISBN == dto.ISBN && b.Id != id))
                throw new IsbnExistException(dto.ISBN);


            _mapper.Map(dto, book);

            book.ChangeTotalCopies(dto.TotalCopies);

            //book.AvailableCopies = dto.TotalCopies;

            _unitOfWork.Books.Update(book);
            await _unitOfWork.CompleteAsync();
        }
    }
}

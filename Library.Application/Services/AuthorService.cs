using AutoMapper;
using Library.Application.Abstractions.Repositories;
using Library.Application.DTOs.AuthorDtos;
using Library.Application.Exceptions.NotFoundExceptions;
using Library.Application.Interfaces;
using Library.Domain.Entities;
using FluentValidation;
using Library.Application.Exceptions.BusinessRuleExceptions;

namespace Library.Application.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateAuthorDto> _validatorCreate;
        private readonly IValidator<UpdateAuthorDto> _validatorUpdate;

        public AuthorService(IUnitOfWork unitOfWork, IMapper mapper,
            IValidator<CreateAuthorDto> validatorCreate, IValidator<UpdateAuthorDto> validatorUpdate)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validatorCreate = validatorCreate;
            _validatorUpdate = validatorUpdate;
        }

        public async Task CreateAuthorAsync(CreateAuthorDto dto)
        {
            await _validatorCreate.ValidateAndThrowAsync(dto);

            var author = _mapper.Map<Author>(dto);
            await _unitOfWork.Authors.AddAsync(author);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAuthorAsync(int id)
        {
            var author = await _unitOfWork.Authors.GetByIdAsync(id);
            if (author == null)
                throw new AuthorNotFoundException(id);

            var hasBooks = await _unitOfWork.Books
                .AnyAsync(b => b.AuthorId == id);

            if (hasBooks)
                throw new AuthorHasBooksException(id);

            _unitOfWork.Authors.Delete(author);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<AuthorDto> GetAuthorByIdAsync(int id)
        {
            var author = await _unitOfWork.Authors.GetByIdAsync(id);
            if (author == null)
                throw new AuthorNotFoundException(id);

            return _mapper.Map<AuthorDto>(author);
        }

        public async Task<IEnumerable<AuthorDto>> GetAuthorsAsync()
        {
            var authors = await _unitOfWork.Authors.GetAllAsync();
            return _mapper.Map<IEnumerable<AuthorDto>>(authors);
        }

        public async Task UpdateAuthorAsync(int id, UpdateAuthorDto dto)
        {
            await _validatorUpdate.ValidateAndThrowAsync(dto);

            var authorOld = await _unitOfWork.Authors.GetByIdAsync(id);
            if (authorOld == null)
                throw new AuthorNotFoundException(id);

            _mapper.Map(dto, authorOld);
            _unitOfWork.Authors.Update(authorOld);
            await _unitOfWork.CompleteAsync();
        }
    }
}

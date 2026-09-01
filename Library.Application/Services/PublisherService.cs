using AutoMapper;
using FluentValidation;
using Library.Application.Abstractions.Repositories;
using Library.Application.DTOs.PublisherDtos;
using Library.Application.Exceptions.NotFoundExceptions;
using Library.Application.Exceptions;
using Library.Application.Interfaces;
using Library.Domain.Entities;
using Library.Application.Exceptions.ExistExceptions;
using Library.Application.Exceptions.BusinessRuleExceptions;

namespace Library.Application.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<CreatePublisherDto> _createValidator;
        private readonly IValidator<UpdatePublisherDto> _updateValidator;

        public PublisherService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IValidator<CreatePublisherDto> createValidator,
            IValidator<UpdatePublisherDto> updateValidator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<IEnumerable<PublisherDto>> GetPublishersAsync()
        {
            var list = await _unitOfWork.Publishers.GetAllAsync();
            return _mapper.Map<IEnumerable<PublisherDto>>(list);
        }

        public async Task<PublisherDto> GetPublisherByIdAsync(int id)
        {
            var publisher = await _unitOfWork.Publishers.GetByIdAsync(id);

            if (publisher is null)
                throw new PublisherNotFoundException(id);

            return _mapper.Map<PublisherDto>(publisher);
        }

        public async Task CreatePublisherAsync(CreatePublisherDto dto)
        {
            await _createValidator.ValidateAndThrowAsync(dto);

            if (await _unitOfWork.Publishers.AnyAsync(p => p.Phone == dto.Phone))
                throw new PhoneExistException(dto.Phone);

            var publisher = _mapper.Map<Publisher>(dto);

            await _unitOfWork.Publishers.AddAsync(publisher);

            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdatePublisherAsync(int id, UpdatePublisherDto dto)
        {
            await _updateValidator.ValidateAndThrowAsync(dto);

            var publisher = await _unitOfWork.Publishers.GetByIdAsync(id);

            if (publisher is null)
                throw new PublisherNotFoundException(id);

            if (await _unitOfWork.Publishers.AnyAsync(p =>
                p.Phone == dto.Phone && p.Id != id))
                    throw new PhoneExistException(dto.Phone);

            _mapper.Map(dto, publisher);

            _unitOfWork.Publishers.Update(publisher);

            await _unitOfWork.CompleteAsync();
        }

        public async Task DeletePublisherAsync(int id)
        {
            var publisher = await _unitOfWork.Publishers.GetByIdAsync(id);

            if (publisher is null)
                throw new PublisherNotFoundException(id);

            var hasBooks = await _unitOfWork.Books
                .AnyAsync(b => b.PublisherId == id);

            if (hasBooks)
                throw new PublisherHasBooksException(id);

            _unitOfWork.Publishers.Delete(publisher);

            await _unitOfWork.CompleteAsync();
        }
    }
}
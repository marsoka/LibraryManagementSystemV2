using AutoMapper;
using FluentValidation;
using Library.Application.Abstractions.Repositories;
using Library.Application.DTOs.CategoryDtos;
using Library.Application.Exceptions.BusinessRuleExceptions;
using Library.Application.Exceptions.NotFoundExceptions;
using Library.Application.Interfaces;
using Library.Domain.Entities;

namespace Library.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateCategoryDto> _validatorCreate;
        private readonly IValidator<UpdateCategoryDto> _validatorUpdate;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper,
            IValidator<CreateCategoryDto> validatorCreate,
            IValidator<UpdateCategoryDto> validatorUpdate)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validatorCreate = validatorCreate;
            _validatorUpdate = validatorUpdate;
        }

        public async Task CreateCategoryAsync(CreateCategoryDto dto)
        {
            await _validatorCreate.ValidateAndThrowAsync(dto);

            var category = _mapper.Map<Category>(dto);

            await _unitOfWork.Categories.AddAsync(category);

            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);

            if (category is null)
                throw new CategoryNotFoundException(id);

            var hasBooks = await _unitOfWork.Books
                .AnyAsync(b => b.CategoryId == id);

            if (hasBooks)
                throw new CategoryHasBooksException(id);

            _unitOfWork.Categories.Delete(category);

            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        {
            var list = await _unitOfWork.Categories.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryDto>>(list);
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(int id)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);

            if (category is null)
                throw new CategoryNotFoundException(id);

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task UpdateCategoryAsync(int id, UpdateCategoryDto dto)
        {
            await _validatorUpdate.ValidateAndThrowAsync(dto);

            var category = await _unitOfWork.Categories.GetByIdAsync(id);

            if (category is null)
                throw new CategoryNotFoundException(id);

            _mapper.Map(dto, category);

            _unitOfWork.Categories.Update(category);

            await _unitOfWork.CompleteAsync();
        }
    }
}
using StoreApi.src.application.DTOs;
using StoreApi.src.domain;
using StoreApi.src.infraestructure;

namespace StoreApi.src.application
{
    public class UpdateCategoryUseCase(CategoryRepository categoryRepository)
    {
        private readonly CategoryRepository _categoryRepository = categoryRepository;

        public async Task<Category> ExecuteAsync(UpdateCategoryDTO updateCategoryDTO, Category category)
        {
            if (!string.IsNullOrWhiteSpace(updateCategoryDTO.Name)) category.Name = updateCategoryDTO.Name;
            if (!string.IsNullOrWhiteSpace(updateCategoryDTO.Icon)) category.Icon = updateCategoryDTO.Icon;

            await _categoryRepository.UpdateCategoryAsync(category);
            return category;
        }
    }
}
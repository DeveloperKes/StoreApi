using StoreApi.src.application.DTOs;
using StoreApi.src.domain;
using StoreApi.src.infraestructure;

namespace StoreApi.src.application
{
    public class FilterProductsUseCase(ProductRepository productRepository, CategoryRepository categoryRepository)
    {
        private readonly ProductRepository _productRepository = productRepository;
        private readonly CategoryRepository _categoryRepository = categoryRepository;

        public async Task<List<Product>> ExecuteAsync(FilterProductsDTO filterProductsDTO)
        {
            var categories = new List<Category>();
            if (filterProductsDTO.CategoriesIds.Count > 0)
            {
                foreach (var categoryId in filterProductsDTO.CategoriesIds)
                {
                    var category = await _categoryRepository.GetCategoryByIdAsync(categoryId);
                    if (category != null) categories.Add(category);
                }
            }

            return await _productRepository.FindBy(categories, filterProductsDTO.PathName);
        }
    }

}
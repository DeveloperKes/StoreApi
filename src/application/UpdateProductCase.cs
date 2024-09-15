using StoreApi.src.application.DTOs;
using StoreApi.src.domain;
using StoreApi.src.infraestructure;

namespace StoreApi.src.application
{
    public class UpdateProductUseCase(ProductRepository productRepository, CategoryRepository categoryRepository)
    {
        private readonly ProductRepository _productRepository = productRepository;
        private readonly CategoryRepository _categoryRepository = categoryRepository;

        public async Task<Product> ExecuteAsync(UpdateProductDTO updateProductDTO, Product product)
        {
            if (!string.IsNullOrWhiteSpace(updateProductDTO.Name)) product.Name = updateProductDTO.Name;
            if (!string.IsNullOrWhiteSpace(updateProductDTO.Image)) product.Image = updateProductDTO.Image;
            if (!string.IsNullOrWhiteSpace(updateProductDTO.Description)) product.Description = updateProductDTO.Description;
            if (updateProductDTO.Price != null && decimal.IsPositive((decimal)updateProductDTO.Price)) product.Price = (decimal)updateProductDTO.Price;
            if (updateProductDTO.Rate != null && decimal.IsPositive((decimal)updateProductDTO.Rate)) product.Rate = (decimal)updateProductDTO.Rate;

            if (updateProductDTO.CategoryIds != null && updateProductDTO.CategoryIds.Count > 0)
            {
                product.Categories = [];
                foreach (var categoryId in updateProductDTO.CategoryIds)
                {
                    var category = await _categoryRepository.GetCategoryByIdAsync(categoryId);
                    if (category != null) product.Categories.Add(category);
                }
            }

            await _productRepository.UpdateProductAsync(product);
            return product;
        }
    }
}
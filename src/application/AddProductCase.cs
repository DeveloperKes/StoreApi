
namespace StoreApi.src.application
{
    using StoreApi.src.infraestructure;
    using StoreApi.src.domain;
    using System.Threading.Tasks;
    public class AddProductUseCase(ProductRepository productRepository, CategoryRepository categoryRepository)
    {
        private readonly ProductRepository _productRepository = productRepository;
        private readonly CategoryRepository _categoryRepository = categoryRepository;

        public async Task ExecuteAsync(Product product, List<int> categoryIds)
        {
            if (product.Price <= 0) throw new ArgumentException("El precio del producto debe ser superior a 0.");
            if (product.Name == "") throw new ArgumentException("El producto no puede tener un nombre vacio.");
            product.Categories = [];
            if (categoryIds != null && categoryIds.Count > 0)
            {
                foreach (var categoryId in categoryIds)
                {
                    var category = await _categoryRepository.GetCategoryByIdAsync(categoryId);
                    if (category != null) product.Categories.Add(category);
                }
            }
            await _productRepository.AddProductAsync(product);
        }
    }
}
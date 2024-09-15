namespace StoreApi.src.application
{
    using StoreApi.src.domain;
    using StoreApi.src.infraestructure;
    using System.Threading.Tasks;

    public class AddCategoryUseCase(CategoryRepository categoryRepository)
    {
        private readonly CategoryRepository _categoryRepository = categoryRepository;

        public async Task ExecuteAsync(Category category)
        {
            if (category.Name == "")
            {
                throw new ArgumentException("No se puede crear una categoría vacía.");
            }

            await _categoryRepository.AddCategoryAsync(category);
        }
    }
}
using StoreApi.src.domain;

namespace StoreApi.src.application.DTOs
{

    public class ProductDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public Boolean IsFavorite { get; set; } = false;
        public decimal Price { get; set; }
        public decimal Rate { get; set; } = 0;
        public string? Description { get; set; }
        public string? Image { get; set; }
        public List<Category> Categories { get; set; } = [];

    }
}
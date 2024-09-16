namespace StoreApi.src.application.DTOs
{
    public class CreateProductDTO
    {
        public required string Name { get; set; }
        public string? Image { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public decimal Rate { get; set; }
        public List<int> CategoryIds { get; set; } = [];
    }
}
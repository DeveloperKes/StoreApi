namespace StoreApi.src.application.DTOs
{
    public class CreateProductDTO
    {
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public List<int> CategoryIds { get; set; } = [];
    }
}
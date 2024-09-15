namespace StoreApi.src.application.DTOs
{
    public class FilterProductsDTO
    {
        public string? PathName { get; set; }
        public List<int> CategoriesIds { get; set; } = [];
    }
}
namespace StoreApi.src.application.DTOs
{
    public class CreateWishProductDTO
    {
        public required int ProductId { get; set; }
        public required int UserId { get; set; }

    }
}
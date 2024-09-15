using StoreApi.src.domain;

namespace StoreApi.src.application.DTOs
{
    public class WishProductResponseDTO
    {
        public int Id { get; set; }
        public required Product Product { get; set; }

        public DateTime DateAdd { get; set; }
    }
}
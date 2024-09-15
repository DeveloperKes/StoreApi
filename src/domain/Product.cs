
using System.Text.Json.Serialization;

namespace StoreApi.src.domain
{
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string Image { get; set; } = "/public/images/notdefined.webp";
        public decimal Price { get; set; }
        public string Description { get; set; } = "El vendedor no ha proporcionado una descripción del producto";
        public decimal Rate { get; set; } = 0;
        public ICollection<Category> Categories { get; set; } = [];
        [JsonIgnore]
        public ICollection<WishProduct> WishProducts { get; private set; } = [];

    }
}
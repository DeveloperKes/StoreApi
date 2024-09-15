
using System.Text.Json.Serialization;

namespace StoreApi.src.domain
{
    public class Category
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string Icon { get; set; } = "code-slash";

        [JsonIgnore]
        public ICollection<Product> Products { get; } = [];
    }
}
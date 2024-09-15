
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace StoreApi.src.domain
{
    [Index(nameof(Username), IsUnique = true)]
    public class User
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public DateTime LastLogin { get; set; }
        public Person? Person { get; set; }

        [JsonIgnore]
        public ICollection<WishProduct> WishProducts { get; set; } = [];
    }
}
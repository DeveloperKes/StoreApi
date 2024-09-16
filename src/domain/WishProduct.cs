using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace StoreApi.src.domain
{
    public class WishProduct
    {
        public int Id { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime DateAdd { get; set; }
        public required User User { get; set; }
        public required Product Product { get; set; }
    }
}
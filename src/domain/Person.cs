
namespace StoreApi.src.domain
{
    public class Person
    {
        public int Id { get; set; }
        public required string Firstname { get; set; }
        public required string Lastname { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
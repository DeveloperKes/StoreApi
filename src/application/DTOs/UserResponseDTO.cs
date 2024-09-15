namespace StoreApi.src.application.DTOs
{
    public class UserResponseDTO
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Firstname { get; set; }
        public required string Lastname { get; set; }
    }
}
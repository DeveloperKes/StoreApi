namespace StoreApi.src.application.DTOs
{
    public class CreateUserDTO
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string Firstname { get; set; }
        public required string Lastname { get; set; }
    }
}
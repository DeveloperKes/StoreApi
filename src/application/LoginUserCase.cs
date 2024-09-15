using StoreApi.src.application.DTOs;
using StoreApi.src.infraestructure;

namespace StoreApi.src.application
{
    public class LoginUserUseCase(UserRepository userRepository, PersonRepository personRepository)
    {
        private readonly UserRepository _userRepository = userRepository;
        private readonly PersonRepository _personRepository = personRepository;

        public async Task<UserResponseDTO?> ExecuteAsync(LoginUserDTO loginUserDTO)
        {
            if (string.IsNullOrWhiteSpace(loginUserDTO.Username)) throw new ArgumentException("No es posible iniciar la sesión sin un nombre de usuario.");
            if (string.IsNullOrWhiteSpace(loginUserDTO.Password)) throw new ArgumentException("No se ha proporcionado una contraseña.");

            var user = await _userRepository.GetUserByUsernameAsync(loginUserDTO.Username);
            if (user != null)
            {
                if (user.Password != loginUserDTO.Password) Results.Forbid();
                var person = await _personRepository.GetPersonByUserAsync(user);
                if (person != null)
                {
                    return new UserResponseDTO
                    {
                        Id = user.Id,
                        Username = user.Username,
                        Firstname = person.Firstname,
                        Lastname = person.Lastname
                    };
                }
            }
            return null;
        }
    }
}

using StoreApi.src.application.DTOs;
using StoreApi.src.domain;
using StoreApi.src.infraestructure;

namespace StoreApi.src.application
{
    public class AddUserUseCase(UserRepository userRepository, PersonRepository personRepository)
    {
        private readonly UserRepository _userRepository = userRepository;
        private readonly PersonRepository _personRepository = personRepository;

        public async Task<UserResponseDTO> ExecuteAsync(CreateUserDTO createUserDTO)
        {
            if (string.IsNullOrWhiteSpace(createUserDTO.Username)) throw new ArgumentException("Es obligatorio definir un nombre de usuario.");
            if (string.IsNullOrWhiteSpace(createUserDTO.Password)) throw new ArgumentException("Es obligatorio definir una contraseña para el usuario.");
            if (string.IsNullOrWhiteSpace(createUserDTO.Firstname)) throw new ArgumentException("Es obligatorio definir un nombre para el usuario.");
            if (string.IsNullOrWhiteSpace(createUserDTO.Lastname)) throw new ArgumentException("Es obligatorio definir un apellido para el usuario.");

            var user = new User
            {
                Username = createUserDTO.Username,
                Password = createUserDTO.Password
            };

            await _userRepository.AddUserAsync(user);

            var person = new Person
            {
                Firstname = createUserDTO.Firstname,
                Lastname = createUserDTO.Lastname,
                User = user
            };

            await _personRepository.AddPersonAsync(person);
            return new UserResponseDTO
            {
                Id = user.Id,
                Username = user.Username,
                Firstname = person.Firstname,
                Lastname = person.Lastname
            };
        }
    }
}
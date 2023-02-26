using System.ComponentModel.DataAnnotations;
using Application.ModelInterfaces.DtoInterfaces;

namespace SharedLibrary.DataTransferObjects.Authentication
{
    public sealed class RegistrationRequest : IRegistrationRequest
    {
        [Required]
        public string Username { get; init; }

        [Required]
        [MinLength(8)]
        public string Password { get; init; }

        public RegistrationRequest(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}

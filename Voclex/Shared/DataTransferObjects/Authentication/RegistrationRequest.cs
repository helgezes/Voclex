using System.ComponentModel.DataAnnotations;
using SharedLibrary.ModelInterfaces.DtoInterfaces;

namespace SharedLibrary.DataTransferObjects.Authentication
{
    public sealed class RegistrationRequest : IRegistrationRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Password should be at least 8 characters long.")]
        public string Password { get; set; }

        public RegistrationRequest(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public RegistrationRequest()
        {
            Username = string.Empty;
            Password = string.Empty;
        }
    }
}

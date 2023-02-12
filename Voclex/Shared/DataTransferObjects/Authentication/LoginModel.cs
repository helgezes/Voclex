using System.ComponentModel.DataAnnotations;

namespace SharedLibrary.DataTransferObjects.Authentication
{
    public sealed class LoginModel
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}

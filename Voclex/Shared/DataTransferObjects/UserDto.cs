using System.ComponentModel.DataAnnotations;
using Application.ModelInterfaces.DtoInterfaces;
using Application.Models;

namespace SharedLibrary.DataTransferObjects
{
    public sealed class UserDto : Dto, IUserDto
    {
        public UserDto(int id, string name, Role role) : base(id)
        {
            Name = name;
            Role = role;
        }


        [Required]
        public string Name { get; }

        [Required]
        public Role Role { get; }
    }
}

using System.ComponentModel.DataAnnotations;
using Application.ModelInterfaces.DtoInterfaces;

namespace SharedLibrary.DataTransferObjects
{
    public sealed class UserDto : Dto, IUserDto
    {
        public UserDto(int id, string name) : base(id)
        {
            Name = name;
        }


        [Required]
        public string Name { get; init; }
    }
}

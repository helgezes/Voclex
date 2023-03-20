using Application.ModelInterfaces.DtoInterfaces;
using Application.Models;
using SharedLibrary.DataTransferObjects;
using System.Security.Claims;

namespace SharedLibrary.Services
{
    public sealed class ClaimsUserConverter
    {
        public static IUserDto ConvertClaimsToUser(IEnumerable<Claim> claims)
        {
            var userId = int.Parse(claims.First(c => c.Type == "id").Value);
            var userName = claims.First(c => c.Type == "name").Value;
            var role = Enum.Parse<Role>(claims.First(c => c.Type == "role").Value);

            return new UserDto(userId, userName, role);
        }

        public static Claim[] GetClaimsFromUser(IUserDto user)
        {
            return new[] { new Claim("id", user.Id.ToString()),
                new Claim("name", user.Name),
                new Claim("role", user.Role.ToString())
            };
        }
    }
}

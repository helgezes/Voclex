﻿using Application.Models;

namespace Application.ModelInterfaces.DtoInterfaces
{
    public interface IUserDto : IIdentifiable
    {
        string Name { get; }

        Role Role { get; }
    }
}

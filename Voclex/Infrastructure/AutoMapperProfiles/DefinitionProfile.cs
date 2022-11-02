using Application.Models;
using AutoMapper;
using SharedLibrary.DataTransferObjects;

namespace Infrastructure.AutoMapperProfiles
{
    public sealed class DefinitionProfile : Profile
    {
        public DefinitionProfile()
        {
            CreateMap<Definition, DefinitionDto>().ReverseMap();
        }
    }
}

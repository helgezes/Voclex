using Application.Models;
using AutoMapper;
using Shared;
using Shared.DataTransferObjects;

namespace Infrastructure.AutoMapperProfiles
{
    public sealed class TermsDictionaryProfile : Profile
    {
        public TermsDictionaryProfile()
        {
            CreateMap<TermsDictionary, TermsDictionaryDto>().ReverseMap();
        }
    }
}

using Application.Models;
using AutoMapper;
using Shared;
using Shared.DataTransferObjects;

namespace Infrastructure.AutoMapperProfiles
{
    public sealed class TermProfile : Profile
    {
        public TermProfile()
        {
            CreateMap<Term, TermDto>().ReverseMap();
        }
    }
}

using Application.Models;
using AutoMapper;
using SharedLibrary.DataTransferObjects;

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

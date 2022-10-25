using Application.Models;
using AutoMapper;
using Shared.DataTransferObjects;

namespace Infrastructure.AutoMapperProfiles
{
    public sealed class PictureProfile : Profile
    {
        public PictureProfile()
        {
            CreateMap<Picture, PictureDto>().ReverseMap();
        }
    }
}
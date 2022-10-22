using Application.Models;
using AutoMapper;
using Shared.DataTransferObjects;

namespace Infrastructure.AutoMapperProfiles
{
    public sealed class ExampleProfile : Profile
    {
        public ExampleProfile()
        {
            CreateMap<Example, ExampleDto>().ReverseMap();
        }
    }
}
using Application.Models;
using AutoMapper;
using SharedLibrary.DataTransferObjects;
using SharedLibrary.ModelInterfaces.DtoInterfaces;

namespace Infrastructure.AutoMapperProfiles;

public sealed class ExceptionLogProfile : Profile
{
    public ExceptionLogProfile()
    {
        CreateMap<ExceptionLogDto, ExceptionLog>()
            .ForMember(x => x.TimeOccurred, 
                x =>
                    x.MapFrom(_ => DateTimeOffset.UtcNow))
            .ReverseMap();

        CreateMap<ExceptionLog, IExceptionLogDto>().As<ExceptionLogDto>();
    }
}


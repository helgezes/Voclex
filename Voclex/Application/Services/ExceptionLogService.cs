using Application.DataAccess;
using Application.Models;
using Application.Services.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.ModelInterfaces.DtoInterfaces;

namespace Application.Services
{
    public sealed class ExceptionLogService
    {
        private readonly IDbContext context;
        private readonly IMapper mapper;
        private readonly ICrudService<ExceptionLog, IExceptionLogDto> crudService;

        public ExceptionLogService(
            IDbContext context, 
            IMapper mapper, 
            ICrudService<ExceptionLog, IExceptionLogDto> crudService)
        {
            this.context = context;
            this.mapper = mapper;
            this.crudService = crudService;
        }

        public async Task Create(IExceptionLogDto dto)
        {
            await crudService.Create(dto);
        }

        public async Task<IExceptionLogDto[]> GetByDates(DateTimeOffset start, DateTimeOffset end)
        {
            return await context.ExceptionLogs
                .Where(e => start <= e.TimeOccurred && e.TimeOccurred <= end)
                .OrderByDescending(e => e.TimeOccurred)
                .ProjectTo<IExceptionLogDto>(mapper.ConfigurationProvider)
                .ToArrayAsync();
        }
    }
}

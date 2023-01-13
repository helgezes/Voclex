﻿using Application.Models;
using Application.Services;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.DataTransferObjects;
using SharedLibrary.Queries.TermsDictionary;

namespace WebApi.Controllers
{
	[Route("[controller]")]
	public class TermsDictionaryController : GenericCrudController<TermsDictionary, TermsDictionaryDto>
    {
        private readonly IGetListService<TermsDictionary, TermsDictionaryDto> listService;

        public TermsDictionaryController(
	        IGetListService<TermsDictionary, TermsDictionaryDto> listService,
            ICrudService<TermsDictionary, TermsDictionaryDto> crudService) : base(crudService)
        {
            this.listService = listService;
        }

        [HttpGet(nameof(GetList))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Task<IEnumerable<TermsDictionaryDto>> GetList([FromQuery] TermsDictionariesListQuery query) 
        {
            return listService.GetAsync(query);
        }

        [HttpGet(nameof(GetCount))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Task<int> GetCount([FromQuery] TermsDictionariesQuery query)
        {
            return listService.GetCount(query);
        }
    }
}

using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    public class TermProgressController : ControllerBase
    {
        private readonly TermProgressService _progressService;
        public TermProgressController(TermProgressService progressService)
        {
            _progressService = progressService;
        }

        [HttpPost(nameof(CorrectGuess))]
        public async Task CorrectGuess([FromQuery] TermProgressDto termProgressDto) //without FromQueryAttribute asp.net core does not respect [BindRequired] on properties 
        {
            await _progressService.CorrectGuess(termProgressDto.TermId, termProgressDto.UserId);
        }

        [HttpPost(nameof(IncorrectGuess))]
        public async Task IncorrectGuess([FromQuery] TermProgressDto termProgressDto)
        {
            await _progressService.IncorrectGuess(termProgressDto.TermId, termProgressDto.UserId);
        }

        [HttpPost(nameof(AlreadyKnow))]
        public async Task AlreadyKnow([FromQuery] TermProgressDto termProgressDto)
        {
            await _progressService.AlreadyKnow(termProgressDto.TermId, termProgressDto.UserId);
        }
    }
}

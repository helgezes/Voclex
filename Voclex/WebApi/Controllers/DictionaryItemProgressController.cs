using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    public class DictionaryItemProgressController : ControllerBase
    {
        private readonly DictionaryItemProgressService _progressService;
        public DictionaryItemProgressController(DictionaryItemProgressService progressService)
        {
            _progressService = progressService;
        }

        [HttpPost(nameof(CorrectGuess))]
        public async Task CorrectGuess(int termId, int userId)
        {
            await _progressService.CorrectGuess(termId, userId);
        }

        [HttpPost(nameof(IncorrectGuess))]
        public async Task IncorrectGuess(int termId, int userId)
        {
            await _progressService.IncorrectGuess(termId, userId);
        }
    }
}

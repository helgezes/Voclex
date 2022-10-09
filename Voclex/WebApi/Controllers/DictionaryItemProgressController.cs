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
        public async Task CorrectGuess(int dictionaryItemId, int userId)
        {
            await _progressService.CorrectGuess(dictionaryItemId, userId);
        }

        [HttpPost(nameof(IncorrectGuess))]
        public async Task IncorrectGuess(int dictionaryItemId, int userId)
        {
            await _progressService.IncorrectGuess(dictionaryItemId, userId);
        }
    }
}

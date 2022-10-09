using Application.DataAccess;
using Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public sealed class DictionaryItemProgressService
    {
        private readonly IDbContext context;

        public DictionaryItemProgressService(IDbContext context)
        {
            this.context = context;
        }

        public async Task CorrectGuess(int termId, int userId)
        {
            var relevantProgressItem = await GetDictionaryItemProgress(termId, userId);
            relevantProgressItem.CorrectGuess();
            await context.SaveChangesAsync();
        }

        public async Task IncorrectGuess(int termId, int userId)
        {
            var relevantProgressItem = await GetDictionaryItemProgress(termId, userId);
            relevantProgressItem.IncorrectGuess();
            await context.SaveChangesAsync();
        }

        private async Task<DictionaryItemProgress> GetDictionaryItemProgress(int dictionaryItemId, int userId)
        {
            return await GetProgressItemFromDbIfExists(dictionaryItemId, userId) ??
                   await CreateNewDictionaryItemProgress(dictionaryItemId, userId);
        }

        private async Task<DictionaryItemProgress?> GetProgressItemFromDbIfExists(int dictionaryItemId, int userId)
        {
            var relevantProgressItem =
                await context.DictionaryItemProgresses
                    .FirstOrDefaultAsync(i =>
                        i.TermId == dictionaryItemId && i.UserId == userId);
            return relevantProgressItem;
        }

        private async Task<DictionaryItemProgress> CreateNewDictionaryItemProgress(int dictionaryItemId, int userId)
        {
            var userFromDbTask = context.Users.FindAsync(userId);
            var termFromDbTask = context.Terms.FindAsync(dictionaryItemId);

            DictionaryItemProgress relevantProgressItem =
                new (await userFromDbTask, await termFromDbTask);

            context.DictionaryItemProgresses.Add(relevantProgressItem);
            return relevantProgressItem;
        }

    }
}

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

        public async Task CorrectGuess(int dictionaryItemId, int userId)
        {
            var relevantProgressItem = await GetDictionaryItemProgress(dictionaryItemId, userId);
            relevantProgressItem.CorrectGuess();
            await context.SaveChangesAsync();
        }

        public async Task IncorrectGuess(int dictionaryItemId, int userId)
        {
            var relevantProgressItem = await GetDictionaryItemProgress(dictionaryItemId, userId);
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
                        i.DictionaryItemId == dictionaryItemId && i.UserId == userId);
            return relevantProgressItem;
        }

        private async Task<DictionaryItemProgress> CreateNewDictionaryItemProgress(int dictionaryItemId, int userId)
        {
            var userFromDbTask = context.Users.FindAsync(userId);
            var dictionaryItemFromDbTask = context.DictionaryItems.FindAsync(dictionaryItemId);

            DictionaryItemProgress relevantProgressItem =
                new (await userFromDbTask, await dictionaryItemFromDbTask);

            context.DictionaryItemProgresses.Add(relevantProgressItem);
            return relevantProgressItem;
        }

    }
}

using Application.DataAccess;
using SharedLibrary.Services.Interfaces;
using System.Security.Authentication;
using Application.Models;
using SharedLibrary.ModelInterfaces.DtoInterfaces;

namespace Application.Services
{
	public sealed class TermsDictionaryService
	{
		private readonly IDbContext context;
		private readonly IAuthenticatedUserService userService;

		public TermsDictionaryService(IDbContext context, IAuthenticatedUserService userService)
		{
			this.context = context;
			this.userService = userService;
		}

		public async Task<int> GetMainDictionaryId()
		{
			var user = await userService.GetCurrentUserOrThrowIfNull();
			
			var dictionaryId = GetFirstDictionaryId(user);

			if (dictionaryId is not default(int)) return dictionaryId;

			return await AddNewDictionaryAndGetId(user);
		}

		private async Task<int> AddNewDictionaryAndGetId(IUserDto user)
		{
			var newTermsDictionary = new TermsDictionary("My dictionary", user.Id);
			context.TermsDictionaries.Add(newTermsDictionary);
			await context.SaveChangesAsync();

			return newTermsDictionary.Id;
		}

		private int GetFirstDictionaryId(IUserDto user)
		{
			var dictionaryId = context.TermsDictionaries
				.Where(d => d.UserId == user.Id)
				.OrderBy(d => d.Id)
				.Select(d => d.Id).FirstOrDefault();
			return dictionaryId;
		}
	}
}

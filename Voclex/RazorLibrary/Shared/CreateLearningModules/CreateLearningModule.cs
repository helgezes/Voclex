using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using RazorLibrary.Services.Interfaces;
using SharedLibrary.DataTransferObjects;
using SharedLibrary.ModelInterfaces;

namespace RazorLibrary.Shared.CreateLearningModules
{
	public abstract class CreateLearningModule<TDto> : ComponentBase, ICreatableLearningModule where TDto : class, ITermRelatedEditable, IIdentifiable
    {
        public CreateLearningModule()
        {
            CurrentEntities = new[] { CreateNewDto() };
        }

        protected TDto FirstEntity => CurrentEntities.First();
        protected readonly TDto[] CurrentEntities;

        protected bool IsModuleCreationEnabled;

        [Inject]
        public IAppHttpClient AppHttpClient { get; set; } = null!;

        protected HttpClient Http => AppHttpClient.ApiClient;

		[Parameter]
        public TermDto? Term { get; set; }

        protected abstract string SaveChangesApiPath { get; }

        protected abstract TDto CreateNewDto();

        public virtual async Task<bool> SaveChanges(int termId)
        {
            if (!IsModuleCreationEnabled) return false;

            var responseTasks = CurrentEntities.Select(e =>
            {
                e.TermId = termId;
                return Http.PostAsJsonAsync(SaveChangesApiPath, e);
            }).ToArray();
            await Task.WhenAll(responseTasks);

            return true;
        }

        protected void EnableModuleCreation()
        {
            IsModuleCreationEnabled = true;
        }

        protected void DisableModuleCreation()
        {
	        IsModuleCreationEnabled = false;
        }
	}

    public interface ICreatableLearningModule
    {
        Task<bool> SaveChanges(int termId);
    }
}

using System.Net.Http.Json;
using Application.ModelInterfaces;
using Microsoft.AspNetCore.Components;

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

        protected bool isModuleCreationEnabled;

        [Inject]
        public HttpClient Http { get; set; } = null!;

        protected abstract string SaveChangesApiPath { get; }

        protected abstract TDto CreateNewDto();

        public virtual async Task SaveChanges(int termId)
        {
            if (!isModuleCreationEnabled) return;

            var responseTasks = CurrentEntities.Select(e =>
            {
                e.TermId = termId;
                return Http.PostAsJsonAsync(SaveChangesApiPath, e);
            }).ToArray();
            await Task.WhenAll(responseTasks);
        }

        protected void EnableModuleCreation()
        {
            isModuleCreationEnabled = true;
        }
    }

    public interface ICreatableLearningModule
    {
        Task SaveChanges(int termId);
    }
}

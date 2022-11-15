using System.Net.Http.Json;
using System.Text.Json;
using Application.ModelInterfaces;
using Microsoft.AspNetCore.Components;
using SharedLibrary.DataTransferObjects.Interfaces;

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

        private bool moduleCreationEnabled;

        [Inject]
        public HttpClient Http { get; set; } = null!;

        protected abstract string SaveChangesApiPath { get; }

        protected abstract TDto CreateNewDto();

        public virtual async Task SaveChanges(int termId)
        {
            if (!moduleCreationEnabled) return;

            var responseTasks = CurrentEntities.Select(e =>
            {
                e.TermId = termId;
                return Http.PostAsJsonAsync(SaveChangesApiPath, e);
            }).ToArray();
            await Task.WhenAll(responseTasks);
        }

        protected void EnableModuleCreation()
        {
            moduleCreationEnabled = true;
        }
    }

    public interface ICreatableLearningModule
    {
        Task SaveChanges(int termId);
    }
}


using Application.ModelInterfaces;
using Microsoft.AspNetCore.Components;

namespace RazorLibrary.Shared.EditLearningModules
{
	public abstract class EditLearningModule<TDto> : ComponentBase, IEditableLearningModule where TDto : ITermRelated, IIdentifiable
    {
        [Inject]
        public HttpClient Http { get; set; } = null!;

        [Parameter]
        public int TermId { get; set; }

        public Task SaveChanges()
        {
            throw new NotImplementedException();
        }
    }

    public interface IEditableLearningModule
    {
        Task SaveChanges();
    }
}

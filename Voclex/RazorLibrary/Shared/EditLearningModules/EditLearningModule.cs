
using System.Net.Http.Json;
using Application.ModelInterfaces;
using Microsoft.AspNetCore.Components;
using RazorLibrary.Helpers;
using SharedLibrary.Queries.TermsRelated;

namespace RazorLibrary.Shared.EditLearningModules
{
	public abstract class EditLearningModule<TDto> : ComponentBase, IEditableLearningModule where TDto : class, ITermRelated, IIdentifiable
    {
        protected TDto? FirstEntity => CurrentEntities?.FirstOrDefault();
        protected TDto[]? CurrentEntities; //if we will have multiple objects for one term. todo will we actually need this?

        [Inject]
        public HttpClient Http { get; set; } = null!;

        [Parameter]
        public int TermId { get; set; }

        [Parameter] 
        public EventCallback<OnInitializationEventArgs> OnInitializationComplete { get; set; }

        protected abstract string GetListApiPath { get; }
        protected abstract string SaveChangesApiPath { get; }

        public virtual async Task SaveChanges()
        {
            var responseTasks = CurrentEntities.Select(e => Http.PutAsJsonAsync(SaveChangesApiPath, e)).ToArray(); //todo make savable only when everything is loaded
            await Task.WhenAll(responseTasks);
        }

        protected override async Task OnInitializedAsync()
        {
            var queryObject = new TermsRelatedListQuery(TermId);
            CurrentEntities =
                await Http.GetFromJsonAsync<TDto[]>($"{GetListApiPath}{queryObject.ObjectPropertiesToQueryString()}");
            
            await OnInitializationComplete.InvokeAsync(new OnInitializationEventArgs(GetType(), FirstEntity != null));
        }
    }

    public interface IEditableLearningModule
    {
        Task SaveChanges();
    }

    public sealed class OnInitializationEventArgs
    {
        public OnInitializationEventArgs(Type type, bool isEntityLoaded)
        {
            Type = type;
            IsEntityLoaded = isEntityLoaded;
        }

        public Type Type { get; init; }

        public bool IsEntityLoaded { get; init; }
    }
}

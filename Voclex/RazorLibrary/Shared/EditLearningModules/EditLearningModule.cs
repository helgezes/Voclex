
using System.Net.Http.Json;
using Application.Queries.TermsRelated;
using Microsoft.AspNetCore.Components;
using RazorLibrary.Helpers;
using RazorLibrary.Services.Interfaces;
using SharedLibrary.DataTransferObjects;
using SharedLibrary.ModelInterfaces;

namespace RazorLibrary.Shared.EditLearningModules
{
	public abstract class EditLearningModule<TDto> : ComponentBase, IEditableLearningModule where TDto : class, ITermRelated, IIdentifiable
    {
        protected TDto? FirstEntity => CurrentEntities?.FirstOrDefault();
        protected TDto[]? CurrentEntities; //if we will have multiple objects for one term. todo will we actually need this?

        [Inject]
        public IAppHttpClient AppHttpClient { get; set; } = null!;

        protected HttpClient Http => AppHttpClient.ApiClient;

		[Parameter]
        public TermDto Term { get; set; } = null!;

        [Parameter] 
        public EventCallback<OnInitializationEventArgs> OnInitializationComplete { get; set; }

        protected abstract string GetListApiPath { get; }
        protected abstract string SaveChangesApiPath { get; }

        public virtual async Task SaveChanges()
        {
            var responseTasks = CurrentEntities.Select(e => Http.PutAsJsonAsync(SaveChangesApiPath, e)).ToArray();
            await Task.WhenAll(responseTasks);
        }

        public async Task LoadEntities()
        {
	        var queryObject = new TermsRelatedListQuery(Term.Id);
	        CurrentEntities =
		        await Http.GetFromJsonAsync<TDto[]>($"{GetListApiPath}{queryObject.ObjectPropertiesToQueryString()}");

	        await OnInitializationComplete.InvokeAsync(new OnInitializationEventArgs(GetType(), FirstEntity != null));
        }

		protected override async Task OnInitializedAsync()
        {
	        await LoadEntities();
        }
    }

    public interface IEditableLearningModule
    {
        Task SaveChanges();
        Task LoadEntities();

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

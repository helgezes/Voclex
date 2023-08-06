using Microsoft.AspNetCore.Components;
using RazorLibrary.Shared.CreateLearningModules;
using RazorLibrary.Shared.EditLearningModules;
using RazorLibrary.Shared;
using SharedLibrary.DataTransferObjects;
using System.Net.Http.Json;
using RazorLibrary.Services.Interfaces;

namespace RazorLibrary.Pages
{
    public partial class EditTermPage : ComponentBase
    {
        [Inject]
        public IAppHttpClient AppHttpClient { get; set; } = null!;

        protected HttpClient Http => AppHttpClient.ApiClient;

        [Parameter]
        public int Id { get; set; }

        private TermDto currentTerm = new(0, string.Empty, 1); //todo dictionary id;

        private readonly CreateEditLearningModule[] createEditLearningModules =
            {
                new(typeof(EditDefinition), typeof(CreateDefinition)),
                new(typeof(EditExamples), typeof(CreateExamples)),
                new(typeof(EditPicture), typeof(CreatePicture))
            };

        private DynamicComponent[] editableLearningModules;
        private readonly Dictionary<Type, DynamicComponent?> creatableLearningModulesDictionary = new();
        private SuccessAlert? successAlert;

        private bool saveEnabled = false;
        private int learningModulesInitializedCount = 0;

        protected override async Task OnInitializedAsync()
        {   //todo mb form as a component, add and edit page separate, using this component
            editableLearningModules = new DynamicComponent[createEditLearningModules.Length];

            TermDto? loadedTerm;
            if (Id != default && (loadedTerm = await Http.GetFromJsonAsync<TermDto>($"Terms?id={Id}")) != null)
                currentTerm = loadedTerm;

        }

        private async Task SaveChanges()
        {
            saveEnabled = false;

            await SendChangesToServer();

            saveEnabled = true;

            successAlert!.Show(5000);
        }

        private async Task SendChangesToServer()
        {
            var responseTask = Http.PutAsJsonAsync("Terms", currentTerm);

            var creatableModulesSaveResultTasks =  //todo refactor
                creatableLearningModulesDictionary.Values.Where(m => m != null)
                .Select(m =>
                {
                    var moduleInstance = (ICreatableLearningModule)m.Instance;
                    var saveChangesTask = moduleInstance.SaveChanges(currentTerm.Id);
                    return new { moduleInstance, saveChangesTask };
                }).ToArray();

            var modulesSaveChangesTasks = editableLearningModules
                .Select(m => ((IEditableLearningModule)m.Instance).SaveChanges())
                .Append(responseTask)
                .Concat(creatableModulesSaveResultTasks.Select(x => x.saveChangesTask))
                .ToArray();

            await Task.WhenAll(modulesSaveChangesTasks);

            foreach (var savingResult in creatableModulesSaveResultTasks)
            {
                if (!await savingResult.saveChangesTask) continue;
                var createdModule = savingResult.moduleInstance;

                LoadCreatedModule(createdModule);
            }
        }

        private void LoadCreatedModule(ICreatableLearningModule createdModule)
        {
            var editModule = createEditLearningModules.First(e => e.Create == createdModule.GetType()).Edit;
            var editComponent = editableLearningModules.First(m => m.Instance.GetType() == editModule);
            ((IEditableLearningModule)editComponent.Instance).LoadEntities();
        }

        private void OnLearningModuleInitialized(OnInitializationEventArgs eventArgs)
        {
            AddCreateModuleIfEntityIsNotLoaded(eventArgs);

            learningModulesInitializedCount++;
            if (learningModulesInitializedCount < createEditLearningModules.Length) return;

            saveEnabled = true;
        }

        private void AddCreateModuleIfEntityIsNotLoaded(OnInitializationEventArgs eventArgs)
        {
            var createEditModule = createEditLearningModules.First(m => m.Edit == eventArgs.Type);
            if (eventArgs.IsEntityLoaded)
            {
                createEditModule.ShowCreateModule = false;
                creatableLearningModulesDictionary.Remove(createEditModule.Create);
				return;
            }

            if (createEditModule.ShowCreateModule) return;

            createEditModule.ShowCreateModule = true;
		}

        protected IDictionary<string, object> GetEditLearningModuleParameters()
        {
            return new Dictionary<string, object>
            {
                { "Term", currentTerm },
                { "OnInitializationComplete",
                    EventCallback.Factory.Create<OnInitializationEventArgs>(this, OnLearningModuleInitialized) }
            };
        }

        protected IDictionary<string, object> GetCreateLearningModuleParameters()
        {
            return new Dictionary<string, object>
            {
                { "Term", currentTerm }
            };
        }

        public sealed class CreateEditLearningModule
        {
            public CreateEditLearningModule(Type edit, Type create)
            {
                Create = create;
                Edit = edit;
            }

            public Type Create { get; }

            public Type Edit { get; }

            public bool ShowCreateModule { get; set; } = false;
        }
    }
}

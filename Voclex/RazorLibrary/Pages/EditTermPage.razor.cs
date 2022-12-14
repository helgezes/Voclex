using Microsoft.AspNetCore.Components;
using RazorLibrary.Shared.CreateLearningModules;
using RazorLibrary.Shared.EditLearningModules;
using RazorLibrary.Shared;
using SharedLibrary.DataTransferObjects;
using System.Net.Http.Json;

namespace RazorLibrary.Pages
{
    public partial class EditTermPage : ComponentBase
    {
        [Parameter]
        public int Id { get; set; }

        private TermDto currentTerm = new(0, string.Empty, 1); //todo dictionary id;

        private readonly Dictionary<Type, Type> editToCreateModules =
            new()
            {
            { typeof(EditDefinition), typeof(CreateDefinition) },
            { typeof(EditExamples), typeof(CreateExamples) },
            { typeof(EditPicture), typeof(CreatePicture) }
            };
        private readonly List<Type> createModules = new();
        private Type[] editModules;

        private DynamicComponent[] editableLearningModules;
        private readonly Dictionary<Type, DynamicComponent?> creatableLearningModulesDictionary = new();
        private SuccessAlert? successAlert;

        private bool saveEnabled = false;
        private int learningModulesInitializedCount = 0;

        protected override async Task OnInitializedAsync()
        {   //todo mb form as a component, add and edit page separate, using this component
            editModules = editToCreateModules.Keys.ToArray();
            editableLearningModules = new DynamicComponent[editToCreateModules.Count];

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
            var editModule = editToCreateModules.First(e => e.Value == createdModule.GetType()).Key;
            var editComponent = editableLearningModules.First(m => m.Instance.GetType() == editModule);
            ((IEditableLearningModule)editComponent.Instance).LoadEntities();
        }

        private void OnLearningModuleInitialized(OnInitializationEventArgs eventArgs)
        {
            AddCreateModuleIfEntityIsNotLoaded(eventArgs);

            learningModulesInitializedCount++;
            if (learningModulesInitializedCount < editModules.Length) return;

            saveEnabled = true;
        }

        private void AddCreateModuleIfEntityIsNotLoaded(OnInitializationEventArgs eventArgs)
        {
            var createModule = editToCreateModules[eventArgs.Type];
            if (eventArgs.IsEntityLoaded)
            {
                createModules.Remove(createModule);
                creatableLearningModulesDictionary.Remove(createModule);
                return;
            }

            if (createModules.Contains(createModule)) return;

            createModules.Add(createModule);
        }

        protected IDictionary<string, object> GetLearningModuleParameters()
        {
            return new Dictionary<string, object>
        {
            { "TermId", Id },
            { "OnInitializationComplete",
                EventCallback.Factory.Create<OnInitializationEventArgs>(this, OnLearningModuleInitialized) }
        };
        }
    }
}

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using RazorLibrary.Shared.CreateLearningModules;
using SharedLibrary.DataTransferObjects;
using System.Net.Http.Json;

namespace RazorLibrary.Pages
{
    public partial class CreateTermPage : ComponentBase
    {
        [Parameter]
        public int DictionaryId { get; set; }

        private readonly Type[] modules = new[] { typeof(CreateDefinition), typeof(CreateExamples), typeof(CreatePicture) };

        private TermDto newTerm = null!;
        private int newTermId = 0;
        private bool saveEnabled;

        private DynamicComponent[] editableLearningModules;
        private IJSObjectReference jsModule;

        private string GoBackToDictionaryUrl => $"/dictionary/{DictionaryId}";
        private string ContinueEditingUrl => $"/term/edit/{newTermId}";

        protected override void OnInitialized()
        {
            editableLearningModules = new DynamicComponent[modules.Length];
            newTerm = new(0, string.Empty, DictionaryId);
            saveEnabled = true;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender) return;

            jsModule = await JS.InvokeAsync<IJSObjectReference>(
                "import", $"./_content/{nameof(RazorLibrary)}/Pages/{nameof(CreateTermPage)}.razor.js");

        }

        private async Task SaveChanges()
        {
            saveEnabled = false;

            var response = await Http.PostAsJsonAsync("Terms", newTerm);
            newTermId = int.Parse(await response.Content.ReadAsStringAsync());

            var modulesSaveChangesTasks = editableLearningModules
                .Select(m => ((ICreatableLearningModule)m.Instance).SaveChanges(newTermId))
                .ToArray();

            await Task.WhenAll(modulesSaveChangesTasks);

            await jsModule.InvokeVoidAsync("ShowSuccessfulCreationModal");
        }

        private async Task DisposeModalAndGoToUrl(string path)
        {
            await jsModule.InvokeVoidAsync("DisposeModalAndGoToUrl", path);
        }

        protected IDictionary<string, object> GetLearningModuleParameters()
        {
            return new Dictionary<string, object>
            {
                { "Term", newTerm }
            };
        }
    }
}

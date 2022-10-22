using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using RazorLibrary.Helpers;
using Shared.DataTransferObjects;
using Shared.Queries.Definitions;

namespace RazorLibrary.Shared.LearningModules
{
    public partial class Definition : ComponentBase
    {
        [Parameter]
        public int CurrentTermId { get; set; }

        [Parameter]
        public int CurrentPage { get; set; }

        [Parameter]
        public int[]? LoadedTermsIds { get; set; }


        private DefinitionDto? currentDefinition;
        private DefinitionDto[]? definitionsForThatPage;
        private int definitionsLoadedForPage = -1;

        protected override async Task OnParametersSetAsync()
        {
            await LoadNewDefinitionsIfNeeded();

            SetCurrentDefinition();
        }

        private async Task LoadNewDefinitionsIfNeeded()
        {
            if (CurrentPage != definitionsLoadedForPage && LoadedTermsIds?.Any() == true)
            {
                definitionsLoadedForPage = CurrentPage;

                await LoadNewDefinitions(LoadedTermsIds);
            }
        }

        private void SetCurrentDefinition()
        {
            if (currentDefinition?.TermId == CurrentTermId || definitionsForThatPage == null) return;

            currentDefinition = definitionsForThatPage.FirstOrDefault(d => d.TermId == CurrentTermId);
        }

        private async Task LoadNewDefinitions(int[] termIds)
        {
            var queryObject = new DefinitionsListQuery(termIds);
            definitionsForThatPage = await Http.GetFromJsonAsync<DefinitionDto[]>(
                $"Definitions/GetList{queryObject.ObjectPropertiesToQueryString()}");
        }

    }
}

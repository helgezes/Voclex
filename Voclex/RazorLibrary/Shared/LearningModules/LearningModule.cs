using System.Net.Http.Json;
using Application.ModelInterfaces;
using Microsoft.AspNetCore.Components;
using RazorLibrary.Helpers;
using Shared.Queries.TermsRelated;

namespace RazorLibrary.Shared.LearningModules;

public abstract class LearningModule<TDto> : ComponentBase where TDto : ITermRelated
{
    [Inject]
    public HttpClient Http { get; set; } = null!;

    [Parameter]
    public int CurrentTermId { get; set; }

    [Parameter]
    public int CurrentPage { get; set; }

    [Parameter]
    public int[]? LoadedTermsIds { get; set; }


    protected TDto? current;
    protected TDto[]? itemsForThatPage;
    protected int itemsLoadedForPage = -1;

    protected abstract string ApiPath { get; }

    protected override async Task OnParametersSetAsync()
    {
        await LoadNewItemsIfNeeded();

        SetCurrent();
    }

    protected async Task LoadNewItemsIfNeeded()
    {
        if (CurrentPage != itemsLoadedForPage && LoadedTermsIds?.Any() == true)
        {
            itemsLoadedForPage = CurrentPage;

            await LoadNewItems(LoadedTermsIds);
        }
    }

    protected void SetCurrent()
    {
        if (current?.TermId == CurrentTermId || itemsForThatPage == null) return;

        current = itemsForThatPage.FirstOrDefault(d => d.TermId == CurrentTermId);
    }

    protected async Task LoadNewItems(int[] termIds)
    {
        var queryObject = new TermsRelatedListQuery(termIds);
        itemsForThatPage = await Http.GetFromJsonAsync<TDto[]>(
            $"{ApiPath}{queryObject.ObjectPropertiesToQueryString()}");
    }
}
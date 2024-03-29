﻿using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using RazorLibrary.Helpers;
using RazorLibrary.Services.Interfaces;
using SharedLibrary.DataTransferObjects.Queries.TermsRelated;
using SharedLibrary.ModelInterfaces;

namespace RazorLibrary.Shared.LearningModules;

public abstract class LearningModule<TDto> : ComponentBase where TDto : ITermRelated
{
    [Inject]
    public IAppHttpClient AppHttpClient { get; set; } = null!;

    [Parameter]
    public int CurrentTermId { get; set; }

    [Parameter]
    public int CurrentPage { get; set; }

    [Parameter]
    public int[]? LoadedTermsIds { get; set; }

    [Parameter]
    public EventCallback<OnLearningModuleLoadingEventArgs> OnLoading { get; set; } //todo separate into two events?


	protected TDto? current;
    protected TDto[]? itemsForThatPage;
    protected int itemsLoadedForPage = -1;

    protected abstract string ApiPath { get; } //todo we can make this virtual and use type name as convention

    protected override async Task OnInitializedAsync()
    {
        await OnLoading.InvokeAsync(new OnLearningModuleLoadingEventArgs(GetType(), false)); //todo refactor?
    }

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

            await OnLoading.InvokeAsync(new OnLearningModuleLoadingEventArgs(GetType(), true));
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
        itemsForThatPage = await AppHttpClient.ApiClient.GetFromJsonAsync<TDto[]>(
            $"{ApiPath}{queryObject.ObjectPropertiesToQueryString()}");
    }
}
public sealed class OnLearningModuleLoadingEventArgs
{
	public OnLearningModuleLoadingEventArgs(Type type, bool isLoadingFinished)
	{
		Type = type;
		IsLoadingFinished = isLoadingFinished;
	}

	public Type Type { get; init; }

	public bool IsLoadingFinished { get; init; }

	public bool IsLoadingStarted => !IsLoadingFinished;
}
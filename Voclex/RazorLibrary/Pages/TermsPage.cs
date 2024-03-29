﻿using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using RazorLibrary.Helpers;
using RazorLibrary.Services.Interfaces;
using SharedLibrary.DataTransferObjects;
using SharedLibrary.DataTransferObjects.Queries.Terms;

namespace RazorLibrary.Pages
{
	public abstract class TermsPage : ComponentBase
    {
        const int pageSize = 7;

        [Inject]
        public IAppHttpClient AppHttpClient { get; set; } = null!;

        protected HttpClient Http => AppHttpClient.ApiClient;

        protected abstract TermsListEnumQueryVariants QueryVariant { get; }

        protected readonly Queue<TermDto> LoadedTerms = new();
        protected TermDto? CurrentTerm;
        
        private int totalPagesCount = int.MaxValue;
        private int currentPage = 1;

        protected override async Task OnInitializedAsync()
        {
            totalPagesCount = await GetTotalPagesCount();
            
            await AddNewTerms(currentPage);

            SetNewCurrentTerm();
        }

        protected virtual void OnTermSet()
        {}

        protected async Task AlreadyKnowThis()
        {
            var response = await PostCurrentTermProgressToPath("TermProgress/AlreadyKnow");

            if (response.IsSuccessStatusCode)
            {
                await SetNewCurrentTermAndLoadIfNeeded();
            }
        }

        protected async Task IncorrectGuess()
        {
            var response = await PostCurrentTermProgressToPath("TermProgress/IncorrectGuess");

            if (response.IsSuccessStatusCode)
            {
                await SetNewCurrentTermAndLoadIfNeeded();
            }
        }

        protected async Task CorrectGuess()
        {
            var response = await PostCurrentTermProgressToPath("TermProgress/CorrectGuess");

            if (response.IsSuccessStatusCode)
            {
                await SetNewCurrentTermAndLoadIfNeeded();
            }
        }

        protected bool IsLastPage()
        {
            return currentPage >= totalPagesCount;
        }

        private async Task SetNewCurrentTermAndLoadIfNeeded()
        {
            SetNewCurrentTerm();
            if (LoadedTerms.Any() || IsLastPage()) return;

            await AddNewTerms(++currentPage);
        }

        private async Task AddNewTerms(int page)
        {
            var newTerms = await GetNewTerms(page);

            foreach (var term in newTerms)
            {
                LoadedTerms.Enqueue(term);
            }
        }

        private async Task<int> GetTotalPagesCount()
        {
            var queryObject = new TermsQueryDto(QueryVariant);

            var newTermsCount = await Http.GetFromJsonAsync<int>(
                $"Terms/GetCount{queryObject.ObjectPropertiesToQueryString()}");

            var totalPageCount = (int)Math.Ceiling(newTermsCount / (decimal)pageSize);
            return totalPageCount;
        }

        private async Task<TermDto[]> GetNewTerms(int page)
        {
            var termsListQuery = new TermsListQueryDto(
                page, pageSize,
                QueryVariant);

            var newTerms = await Http.GetFromJsonAsync<TermDto[]>(
                $"Terms/GetList{termsListQuery.ObjectPropertiesToQueryString()}");

            return newTerms;
        }

        private void SetNewCurrentTerm()
        {
            LoadedTerms.TryDequeue(out CurrentTerm);
            OnTermSet();
        }

        private async Task<HttpResponseMessage> PostCurrentTermProgressToPath(string path)
        {
            var termProgressDtoAsQueryString = GetCurrentTermProgressDtoAsQueryString();
            var response = await Http.PostAsync(
                $"{path}{termProgressDtoAsQueryString}", null);
            return response;
        }

        private string GetCurrentTermProgressDtoAsQueryString()
        {
            var queryObject = new TermProgressDto(CurrentTerm.Id);
            var termProgressDtoAsQueryString = queryObject.ObjectPropertiesToQueryString();
            return termProgressDtoAsQueryString;
        }

        protected IDictionary<string, object> GetLearningModuleParameters()
        {
            return new Dictionary<string, object>
        {
            {
                "CurrentTermId", CurrentTerm.Id
            },
            {
                "LoadedTermsIds", LoadedTerms.Select(t => t.Id).Append(CurrentTerm.Id).ToArray()
            },
            {
                "CurrentPage", currentPage
            }
        };
        }
    }
}

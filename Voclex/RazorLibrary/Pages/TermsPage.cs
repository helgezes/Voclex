﻿using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using RazorLibrary.Helpers;
using Shared.DataTransferObjects;
using Shared.Queries.Terms;

namespace RazorLibrary.Pages
{
	public abstract class TermsPage : ComponentBase
    {
        const int pageSize = 7;

        [Inject]
        public HttpClient Http { get; set; } = null!;

        protected abstract TermsListEnumQueryVariants QueryVariant { get; }

        protected readonly Queue<TermDto> LoadedTerms = new();
        protected TermDto? CurrentTerm;

        private readonly int userId = 1; //todo settings
        private readonly int[] dictionariesIds = new[] { 1, 2 };

        private int totalPagesCount;
        private int currentPage = 1;

        protected override async Task OnInitializedAsync()
        {
            totalPagesCount = await GetTotalPagesCount();

            await AddNewTerms(currentPage);

            SetNewCurrentTerm();
        }


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

        private async Task SetNewCurrentTermAndLoadIfNeeded()
        {
            SetNewCurrentTerm();
            if (LoadedTerms.Any() || currentPage >= totalPagesCount) return;

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
            var queryObject = new TermsQuery(userId,
                QueryVariant,
                dictionariesIds);

            var newTermsCount = await Http.GetFromJsonAsync<int>(
                $"Terms/GetCount{queryObject.ObjectPropertiesToQueryString()}");

            var totalPageCount = (int)Math.Ceiling(newTermsCount / (decimal)pageSize);
            return totalPageCount;
        }

        private async Task<TermDto[]> GetNewTerms(int page)
        {
            var termsListQuery = new TermsListQuery(
                page, pageSize,
                userId,
                QueryVariant,
                dictionariesIds);

            var newTerms = await Http.GetFromJsonAsync<TermDto[]>(
                $"Terms/GetList{termsListQuery.ObjectPropertiesToQueryString()}");

            return newTerms;
        }

        private void SetNewCurrentTerm()
        {
            LoadedTerms.TryDequeue(out CurrentTerm);
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
            var queryObject = new TermProgressDto(CurrentTerm.Id, userId);
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
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Shared.Queries.TermsRelated
{
	public sealed class TermsRelatedListQuery
	{
        public TermsRelatedListQuery(int[] termsIds)
        {
            this.TermsIds = termsIds;
        }

        public TermsRelatedListQuery(){}

        [BindRequired]
        public int[] TermsIds { get; init; }
	}
}

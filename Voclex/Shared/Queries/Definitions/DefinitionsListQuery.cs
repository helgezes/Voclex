using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Shared.Queries.Definitions
{
	public sealed class DefinitionsListQuery
	{
        public DefinitionsListQuery(int[] termsIds)
        {
            this.TermsIds = termsIds;
        }

        public DefinitionsListQuery(){}

        [BindRequired]
        public int[] TermsIds { get; init; }
	}
}


using System.ComponentModel.DataAnnotations;

namespace Shared.Queries.TermsRelated
{
	public sealed class TermsRelatedListQuery
	{
        public TermsRelatedListQuery(int[] termsIds)
        {
            this.TermsIds = termsIds;
        }

        public TermsRelatedListQuery(){}

        [Required]
        public int[] TermsIds { get; init; }
	}
}

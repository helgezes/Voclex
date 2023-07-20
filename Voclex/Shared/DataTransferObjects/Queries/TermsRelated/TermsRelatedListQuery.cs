using System.ComponentModel.DataAnnotations;

namespace SharedLibrary.DataTransferObjects.Queries.TermsRelated
{
	public sealed class TermsRelatedListQuery
	{
        public TermsRelatedListQuery(params int[] termsIds)
        {
            this.TermsIds = termsIds;
        }

        public TermsRelatedListQuery(){}

        [Required]
        public int[] TermsIds { get; init; }
	}
}

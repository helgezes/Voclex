using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
	public sealed class TermsDictionaryDto : Dto
	{
        public TermsDictionaryDto(int id, string title) : base(id)
        {
            Title = title;
        }

        [Required]
        public string Title { get; init; }
    }
}

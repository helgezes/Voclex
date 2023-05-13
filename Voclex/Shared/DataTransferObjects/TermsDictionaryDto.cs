using System.ComponentModel.DataAnnotations;
using SharedLibrary.Attributes;

namespace SharedLibrary.DataTransferObjects
{
	public sealed class TermsDictionaryDto : Dto
	{
        public TermsDictionaryDto(int id, string title) : base(id)
        {
            Title = title;
        }

        [Required]
        public string Title { get; set; }

        [UserId]
        public int UserId { get; set; }
    }
}

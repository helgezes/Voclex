using SharedLibrary.Attributes;

namespace SharedLibrary.DataTransferObjects.Queries.TermsDictionary
{
	public class TermsDictionariesQueryDto
	{
        [UserId]
        public int UserId { get; init; }
    }
}

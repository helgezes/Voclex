using Application.ModelInterfaces;

namespace SharedLibrary.DataTransferObjects
{
    public sealed class PictureDto : ITermRelated
    {
        public PictureDto(int termId, string path)
        {
            TermId = termId;
            Path = path;
        }

        public int TermId { get; init; }
        
        public string Path { get; init; }
    }
}

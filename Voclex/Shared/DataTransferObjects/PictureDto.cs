using Application.ModelInterfaces;

namespace SharedLibrary.DataTransferObjects
{
    public sealed class PictureDto : ITermRelated, IIdentifiable
    {
        public PictureDto(int id, int termId, string path)
        {
            Id = id;
            TermId = termId;
            Path = path;
        }

        public int Id { get; init; }

        public int TermId { get; init; }
        
        public string Path { get; init; }
    }
}

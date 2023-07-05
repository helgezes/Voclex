using SharedLibrary.ModelInterfaces.DtoInterfaces;

namespace SharedLibrary.DataTransferObjects
{
    public sealed class PictureDto : IPictureDto
    {
        public PictureDto(int id, int termId, string path)
        {
            Id = id;
            TermId = termId;
            Path = path;
        }

        public PictureDto(){}

        public int Id { get; init; }

        public int TermId { get; set; }

        public string? Path { get; set; } = string.Empty;
    }
}

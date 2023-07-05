namespace SharedLibrary.ModelInterfaces.DtoInterfaces;

public interface IPictureDto : ITermRelatedEditable, IIdentifiable
{
    string? Path { get; set; }
}
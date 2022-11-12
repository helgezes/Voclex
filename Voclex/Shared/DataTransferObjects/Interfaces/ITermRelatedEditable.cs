using Application.ModelInterfaces;

namespace SharedLibrary.DataTransferObjects.Interfaces
{

    public interface ITermRelatedEditable : ITermRelated
    {
        int TermId { set; }
    }
}

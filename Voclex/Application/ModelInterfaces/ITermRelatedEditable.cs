namespace Application.ModelInterfaces
{

    public interface ITermRelatedEditable : ITermRelated
    {
        public int TermId { set; }
    }
}

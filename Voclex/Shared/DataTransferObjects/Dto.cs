using Application.ModelInterfaces;

namespace SharedLibrary.DataTransferObjects
{
    public abstract class Dto : IIdentifiable
    {
        protected Dto(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}

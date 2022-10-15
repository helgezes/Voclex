using Application.ModelInterfaces;

namespace Application.Models
{
    public abstract class Entity : IIdentifiable
    {
        public int Id { get; protected set; }
    }
}

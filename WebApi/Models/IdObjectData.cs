using BusinessEntities.Base;

namespace WebApi.Models
{
    public abstract record IdObjectData
    {
        public Guid? Identifier { get; set; }

        public IdObjectData(Guid? id)
        {
            Identifier = id;
        }

        public IdObjectData(IdObject entity) : this(entity?.Identifier)
        {
        }

        
    }
}
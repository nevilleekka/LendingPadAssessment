using BusinessEntities.Base;

namespace Core.Factories.Base;

public interface IIdObjectFactory<out T> : IFactory<T> where T : IdObject
{
    T Create(Guid id);
}

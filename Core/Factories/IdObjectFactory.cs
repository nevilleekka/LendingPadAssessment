using BusinessEntities.Base;
using Common;
using Common.Attributes;
using Common.Utilities;
using Core.Factories.Base;

namespace Core.Factories;

[AutoRegister(AutoRegisterTypes.Singleton)]
public class IdObjectFactory<T> : Factory<T>, IIdObjectFactory<T> where T : IdObject
{
    public T Create(Guid id)
    {
        IdObject instance = base.Create();
        instance.Identifier = id;
      
        return (T)instance;
    }
}

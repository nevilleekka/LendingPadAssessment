using BusinessEntities.Base;
using Common;
using Common.Attributes;
using Core.Factories.Base;

namespace Core.Factories;

[AutoRegister(AutoRegisterTypes.Singleton)]
public class Factory<T> : IFactory<T> where T : IdObject
{
    public T Create()
    {
        return Activator.CreateInstance(typeof(T)) as T;
    }
}

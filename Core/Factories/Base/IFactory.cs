namespace Core.Factories.Base;

public interface IFactory<out T>
{
    T Create();
}

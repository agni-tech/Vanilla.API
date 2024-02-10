namespace Vanilla.Shared.Interfaces;
public interface IContainer
{
    T GetService<T>(Type type);
}
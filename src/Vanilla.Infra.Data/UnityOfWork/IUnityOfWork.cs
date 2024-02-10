namespace Vanilla.Infra.Data.UnityOfWork
{
    public interface IUnityOfWork
    {
        Task SaveChanges();
    }
}

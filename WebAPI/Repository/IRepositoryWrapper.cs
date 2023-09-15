using WebAPI.Context;

namespace WebAPI.Repository
{
    public interface IRepositoryWrapper
    {
        ApplicationContext GetContext();
        Task SaveAsync();
    }
}

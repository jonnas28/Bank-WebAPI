using WebAPI.Context;
using WebAPI.Repository.Account;
using WebAPI.Repository.Transaction;

namespace WebAPI.Repository
{
    public interface IRepositoryWrapper
    {
        IAccountRepository Account {  get; }
        ITransactionRepository Transaction { get; }
        ApplicationContext GetContext();
        Task SaveAsync();
    }
}

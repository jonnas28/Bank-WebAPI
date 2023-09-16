using WebAPI.Context;
using WebAPI.Repository.Account;
using WebAPI.Repository.Transaction;

namespace WebAPI.Repository
{
    /// <summary>
    /// Provides a wrapper interface for accessing various repositories and the application context.
    /// </summary>
    public interface IRepositoryWrapper
    {
        IAccountRepository Account {  get; }
        ITransactionRepository Transaction { get; }
        /// <summary>
        /// Gets the application context.
        /// </summary>
        ApplicationContext GetContext();
        /// <summary>
        /// Asynchronously saves changes made to the repositories.
        /// </summary>
        Task SaveAsync();
    }
}

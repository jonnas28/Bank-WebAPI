using Bank.Common.Parameters;
using Helper.Common;

namespace WebAPI.Repository.Account
{
    public interface IAccountRepository : IRepositoryBase<WebAPI.Models.Account>
    {
        Task<PagedList<WebAPI.Models.Account>> GetAll(AccountParameters parameter);
        Task<WebAPI.Models.Account> GetById(Guid Id);
        Task<Models.Account> GetByAccountNumber(string AccountNumber);
        Task<decimal> GetAccountBalance(string accountNumber);
    }
}

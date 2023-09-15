using Bank.Common.Parameters;
using Helper.Common;
using Microsoft.EntityFrameworkCore;
using WebAPI.Context;

namespace WebAPI.Repository.Account
{
    public class AccountRepository : RepositoryBase<WebAPI.Models.Account>, IAccountRepository
    {
        public AccountRepository(ApplicationContext _applicationDbContext) : base(_applicationDbContext)
        {
        }

        public async Task<PagedList<Models.Account>> GetAll(AccountParameters parameter)
        {
            IQueryable<Models.Account> query = FindAll();

            return await PagedList<Models.Account>.ToPagedListAsync(query, parameter.PageNumber, parameter.PageSize);
        }

        public async Task<Models.Account> GetById(Guid Id)
        {
            return await FindByCondition(x => x.Id.Equals(Id))
            .FirstOrDefaultAsync();
        }

        public async Task<Models.Account> GetByAccountNumber(string AccountNumber)
        {
            return await FindByCondition(x => x.AccountNumber.Equals(AccountNumber))
            .FirstOrDefaultAsync();
        }

        public async Task<decimal> GetAccountBalance(string accountNumber)
        {
            return await FindByCondition(x => x.AccountNumber.Equals(accountNumber))
                .Select(x=>x.Balance)
            .FirstOrDefaultAsync();
        }
    }
}

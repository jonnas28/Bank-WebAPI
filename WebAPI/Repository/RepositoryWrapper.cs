using WebAPI.Context;
using WebAPI.Repository.Account;
using WebAPI.Repository.Transaction;

namespace WebAPI.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private IAccountRepository _account;
        private ITransactionRepository _transaction;
        ApplicationContext _context;
        public RepositoryWrapper(ApplicationContext context)
        {
            _context = context;
        }

        public IAccountRepository Account
        {
            get
            {
                if (_account == null)
                {
                    _account = new AccountRepository(_context);
                }
                return _account;
            }
        }

        public ITransactionRepository Transaction
        {
            get
            {
                if(_transaction == null)
                {
                    _transaction = new TransactionRepository(_context);
                }
                return _transaction;
            }
        }

        public ApplicationContext GetContext()
        {
            return _context;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

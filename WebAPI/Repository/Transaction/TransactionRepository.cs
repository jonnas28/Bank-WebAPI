using WebAPI.Context;

namespace WebAPI.Repository.Transaction
{
    public class TransactionRepository : RepositoryBase<WebAPI.Models.Transaction>, ITransactionRepository
    {
        public TransactionRepository(ApplicationContext _applicationDbContext) : base(_applicationDbContext)
        {
            
        }
    }
}

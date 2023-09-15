using Bank.Common;
using WebAPI.Repository.Transaction;

namespace WebAPI.Repository.Account.Command
{
    public class DepositCommandHandler
    {
        private readonly IRepositoryWrapper _repository;
        public DepositCommandHandler(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        public async Task<Models.Transaction> HandleAsync(DepositCommand command)
        {
            // Validate the deposit request
            var account = await _repository.Account.GetByAccountNumber(command.AccountNumber);
            if (account == null)
            {
                throw new ArgumentException("Account Number doesn't exist");
            }

            // Create a transaction DTO for the deposit
            var depositTransaction = new Models.Transaction
            {
                Amount = command.Amount,
                DestinationAccountId = account.Id,
                SourceAccountId = account.Id,
                IsDebit = false,
                Timestamp = DateTime.UtcNow,
                TransactionType = "Deposit",
            };

            account.Balance += command.Amount;

            // Update Account Balance
            _repository.Account.Update(account);

            // Perform the deposit
            _repository.Transaction.Create(depositTransaction);

            await _repository.SaveAsync();

            return depositTransaction;
        }
    }
}

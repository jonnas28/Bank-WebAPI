
namespace WebAPI.Repository.Transaction.Command
{
    public class WithdrawCommandHandler
    {

        private readonly IRepositoryWrapper _repository;
        public WithdrawCommandHandler(IRepositoryWrapper repository)
        {
            _repository = repository;
        }
        public async Task<Models.Transaction> HandleAsync(WithdrawCommand command)
        {
            // Validate the deposit request
            var account = await _repository.Account.GetByAccountNumber(command.AccountNumber);
            if (account == null)
            {
                throw new ArgumentException("Account Number doesn't exist");
            }

            if(account.Balance < command.Amount)
            {
                throw new ArgumentException("Insufficient funds");
            }

            // Create a transaction DTO for the deposit
            var withdrawTransaction = new Models.Transaction
            {
                Amount = command.Amount,
                DestinationAccountId = account.Id,
                SourceAccountId = account.Id,
                IsDebit = true,
                Timestamp = DateTime.UtcNow,
                TransactionType = "Withdraw",
            };

            account.Balance -= command.Amount;

            // Update Account Balance
            _repository.Account.Update(account);

            // Perform the deposit
            _repository.Transaction.Create(withdrawTransaction);

            await _repository.SaveAsync();

            return withdrawTransaction;
        }
    }
}

using WebAPI.Models;

namespace WebAPI.Repository.Transaction.Command
{
    public class TransferCommandHandler
    {
        private readonly IRepositoryWrapper _repository;
        public TransferCommandHandler(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        public async Task<Models.Transaction> HandleAsync(TransferCommand command)
        {
            // Validate the account number
            var sourceAccount = await _repository.Account.GetByAccountNumber(command.SourceAccountNumber);
            if (sourceAccount == null)
            {
                throw new ArgumentException("Source Account Number doesn't exist");
            }
            var destinationAccount = await _repository.Account.GetByAccountNumber(command.DestinationAccountNumber);
            if (destinationAccount == null)
            {
                throw new ArgumentException("Destination Account Number doesn't exist");
            }
            if (sourceAccount.Balance < command.Amount)
            {
                throw new ArgumentException("Insufficient balance.");
            }

            // Create a transaction DTO for the transfer
            var depositTransaction = new Models.Transaction
            {
                Amount = command.Amount,
                DestinationAccountId = destinationAccount.Id,
                SourceAccountId = sourceAccount.Id,
                IsDebit = true,
                Timestamp = DateTime.UtcNow,
                TransactionType = "Transfer",
            };

            //Update Source Account Balance
            sourceAccount.Balance -= command.Amount;
            _repository.Account.Update(sourceAccount);

            //Update destinationAccount
            destinationAccount.Balance += command.Amount;
            _repository.Account.Update(destinationAccount);

            await _repository.SaveAsync();

            return depositTransaction;
        }
    }
}

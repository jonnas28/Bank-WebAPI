using Bank.Common;
using FluentValidation;
using WebAPI.Context;

namespace WebAPI.Validators
{
    public class TransactionValidator : AbstractValidator<TransactionDTO>
    {
        ApplicationContext _context;
        public TransactionValidator(ApplicationContext context)
        {
            _context = context;

            RuleFor(x => x.TransactionType)
                .NotEmpty()
                .NotNull();


            var conditions = new List<string>() { "Deposit", "Withdrawal", "Transfer"};
            RuleFor(x=>x.TransactionType)
                .Must(x => conditions.Contains(x))
                .WithMessage("Allowed Transaction Type: " + String.Join(",", conditions));
        }
    }
}

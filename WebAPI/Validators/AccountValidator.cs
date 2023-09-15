using Bank.Common;
using FluentValidation;
using WebAPI.Context;

namespace WebAPI.Validators
{
    public class AccountValidator : AbstractValidator<AccountDTO>
    {
        ApplicationContext _context;
        public AccountValidator(ApplicationContext context)
        {
            _context = context;

            RuleFor(x => x.AccountNumber)
                .NotNull()
                .Length(16);

            RuleFor(x => x)
            .Must((account, x) =>
            {
                if (x.Id == Guid.Empty)
                {
                    return !_context.Accounts.Any(a => account.AccountNumber.ToUpper() == a.AccountNumber.ToUpper());
                }

                return true;
            })
            .WithErrorCode("AlreadyExists")
            .WithMessage(x => $"{x.AccountNumber} already exists");
        }
    }
}

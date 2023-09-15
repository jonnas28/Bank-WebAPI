using WebAPI.Context;

namespace WebAPI.Validators
{
    public class DepositValidator : TransactionValidator
    {
        public DepositValidator(ApplicationContext context) : base(context)
        {
        }
    }
}

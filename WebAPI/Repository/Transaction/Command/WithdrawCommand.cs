namespace WebAPI.Repository.Transaction.Command
{
    public class WithdrawCommand
    {
        public string AccountNumber { get; set; }
        public decimal Amount { get; set; }
    }
}

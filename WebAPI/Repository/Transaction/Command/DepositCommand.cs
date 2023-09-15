namespace WebAPI.Repository.Transaction.Command
{
    public class DepositCommand
    {
        public string AccountNumber { get; set; }
        public decimal Amount { get; set; }
    }
}

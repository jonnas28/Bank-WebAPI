namespace WebAPI.Repository.Transaction.Command
{
    public class TransferCommand
    {
        public string SourceAccountNumber { get; set; }
        public string DestinationAccountNumber { get; set; }
        public decimal Amount { get; set; }
    }
}

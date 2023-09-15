namespace Bank.Common
{
    public class TransactionDTO
    {
        public int Id { get; set; }
        public string? TransactionType { get; set; }
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsDebit { get; set; }
        public Guid SourceAccountId { get; set; }
        public Guid DestinationAccountId { get; set; }

    }
}

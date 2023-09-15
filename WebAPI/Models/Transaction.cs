using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{

    /// <summary>
    /// Represents a transaction in the system.
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// Gets or sets the unique identifier for the transaction.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the type of the transaction (Deposit, Withdrawal, Transfer).
        /// </summary>
        public string? TransactionType { get; set; }

        /// <summary>
        /// Gets or sets the amount involved in the transaction.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the timestamp of the transaction.
        /// </summary>
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Gets or sets a flag indicating whether this transaction is a debit.
        /// </summary>
        public bool IsDebit { get; set; }

        /// <summary>
        /// Gets or sets the source account ID for the transaction (applicable for Withdrawal and Transfer).
        /// </summary>
        [ForeignKey("SourceAccount, SourceAccountId")]
        public Guid SourceAccountId { get; set; }
        public Account SourceAccount { get; set; }

        /// <summary>
        /// Gets or sets the source account ID for the transaction (applicable for Withdrawal and Transfer).
        /// </summary>
        [ForeignKey("DestinationAccount, DestinationAccountId")]
        public Guid DestinationAccountId { get; set; }
        public Account DestinationAccount { get; set; }


    }

}

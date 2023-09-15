using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    /// <summary>
    /// Represents a bank account.
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Gets or sets the unique identifier for the account.
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the account number.
        /// </summary>
        [Required]
        public string? AccountNumber { get; set; }

        /// <summary>
        /// Gets or sets the balance in the account.
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// Gets or sets the user ID associated with this account.
        /// </summary>
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public User? User { get; set; }

        [InverseProperty(nameof(Transaction.SourceAccount))]
        public ICollection<Transaction> SourceTransactions { get; set; }

        [InverseProperty(nameof(Transaction.DestinationAccount))]
        public ICollection<Transaction> DestinationTransactions { get; set; }
    }
}

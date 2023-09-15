using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Common
{
    public class AccountDTO
    {
        public Guid Id { get; set; }
        public string? AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public string? UserId { get; set; }
    }
}

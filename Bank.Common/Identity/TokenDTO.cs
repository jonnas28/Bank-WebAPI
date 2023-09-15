using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Common.Identity
{
    public class TokenDTO
    {
        public string token { get; set; }
        public DateTime expiration { get; set; }
    }
}

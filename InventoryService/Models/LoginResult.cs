using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryService.Models
{
    public class LoginResult
    {
        public string Token { get; set; }
        public DateTime Expiry { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryWala.DomainLayer.Models.Single
{
    public class TokenViewModel
    {
        public string Token { get; set; }

        public DateTime Expire { get; set; }
    }
}

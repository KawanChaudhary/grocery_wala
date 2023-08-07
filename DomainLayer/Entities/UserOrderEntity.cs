using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryWala.DomainLayer.Entities
{
    public class UserOrderEntity
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public string CouponCode { get; set; }
        public int ExtraDiscount { get; set; }
        public decimal TotalMRP { get; set; }
        public decimal DiscountOnMRP { get; set; }
        public decimal FinalAmount { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}

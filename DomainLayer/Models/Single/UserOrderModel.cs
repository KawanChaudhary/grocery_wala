using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryWala.DomainLayer.Models.Single
{
    public class UserOrderModel
    {
        [Key]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Required user id.")]
        public string UserId { get; set; }
        [Required(ErrorMessage = "Required total MRP.")]
        public decimal TotalMRP { get; set; }
        [Required(ErrorMessage = "Required discount on MRP.")]
        public decimal DiscountOnMRP { get; set; }
        [Required(ErrorMessage = "Required final amount.")]
        public decimal FinalAmount { get; set; }
        public string CouponCode { get; set; }
        public int ExtraDiscount { get; set; }

        public DateTime CreatedAt { get; set; }

    }
}

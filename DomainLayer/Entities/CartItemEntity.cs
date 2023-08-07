using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryWala.DomainLayer.Entities
{
    public class CartItemEntity
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }
        public string ProductId { get; set; }

        public int Quantity { get; set; }
    }
}

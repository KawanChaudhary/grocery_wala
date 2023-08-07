using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryWala.DomainLayer.Models.Single
{
    public class OrderModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Order Id is required")]
        public int OrderId { get; set; }
        [Required(ErrorMessage = "User Id is required")]
        public string UserId {get; set; }
        [Required(ErrorMessage = "Product Id is required")]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Product anme is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Product quantity is required")]
        public int ProductQuantity { get; set; }
        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Diiscount is required")]
        public decimal Discount { get; set; }
        public string ImageAddress { get; set; }
    }
}

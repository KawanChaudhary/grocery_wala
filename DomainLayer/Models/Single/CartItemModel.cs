using System.ComponentModel.DataAnnotations;

namespace GroceryWala.DomainLayer.Models.Single
{
    public class CartItemModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "User id is requried.")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Product id is requried.")]
        public string ProductId { get; set; }

        [Required(ErrorMessage = "Product quantity is requried.")]
        public int Quantity { get; set; }
    }
}

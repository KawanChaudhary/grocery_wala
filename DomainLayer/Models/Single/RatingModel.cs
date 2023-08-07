using System.ComponentModel.DataAnnotations;

namespace GroceryWala.DomainLayer.Models.Single
{
    public  class RatingModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "user id is required")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Product Id required")]
        public string ProductId { get; set; }

        [Required(ErrorMessage = "Rating of product is required")]
        public int Rating { get; set; }
    }

}

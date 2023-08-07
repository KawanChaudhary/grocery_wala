using GroceryWala.DomainLayer.Other;
using System;
using System.ComponentModel.DataAnnotations;

namespace GroceryWala.DomainLayer.Models.Single
{
    public class ProductModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "The Name field is required.")]
        public string Name { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "The Price field must be a positive number.")]
        public decimal Price { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "The Quantity field must be a positive number.")]
        public decimal Quantity { get; set; }

        [Required(ErrorMessage = "The Category field is required.")]
        public CategoryType Category { get; set; }

        [Required(ErrorMessage = "The Description is required.")]
        [StringLength(500, ErrorMessage = "The Description field cannot exceed 255 characters.")]
        public string Description { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "The Stock field must be a non-negative number.")]
        public int Stock { get; set; }

        [Range(0, 100, ErrorMessage = "The Stock field must be a non-negative number.")]
        public int Discount { get; set; }

        [Required]
        public SizeType SizeType { get; set; }

        public int Rating { get; set; }

        public int TotalRatings { get; set; }

        [StringLength(500, ErrorMessage = "The other details field cannot exceed 255 characters.")]
        public string OtherDetails { get; set; }

        public int ReviewCount { get; set; }
    }
}

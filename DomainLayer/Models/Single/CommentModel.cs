using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryWala.DomainLayer.Models.Single
{
    public class CommentModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "User Id is required.")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required(ErrorMessage = "Product Id is required.")]
        public string ProductId { get; set; }

        [Required(ErrorMessage = "Text is required.")]
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

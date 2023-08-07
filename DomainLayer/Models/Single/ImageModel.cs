using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryWala.DomainLayer.Models.Single
{
    public class ImageModel
    {
        [Key]
        public int Id { get; set; }
        public string ProductId { get; set; }
        public string ImageAddress { get; set; }

    }
}

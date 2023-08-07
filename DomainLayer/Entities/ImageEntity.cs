using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryWala.DomainLayer.Entities
{
    public class ImageEntity
    {
        public int Id { get; set; }
        public string ProductId { get; set; }
        public string ImageAddress { get; set; }
    }
}

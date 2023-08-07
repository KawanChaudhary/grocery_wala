using GroceryWala.DomainLayer.Models.Single;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryWala.DomainLayer.Models.Multiple
{
    public class AllProductModel
    {
        public ProductModel Details { get; set; }
        public ImageModel Images { get; set; }

        public bool IsInCart { get; set; }
    }
}

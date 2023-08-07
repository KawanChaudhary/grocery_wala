using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryWala.DomainLayer.Models.Single
{
    public class MostOrderProductModel
    {
        public ProductModel Product { get; set; }

        public int Quantity { get; set; }

        public string ImageAddress { get; set; }

    }
}

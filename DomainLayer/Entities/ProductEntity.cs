using GroceryWala.DomainLayer.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryWala.DomainLayer.Entities
{
    public class ProductEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
        public CategoryType Category { get; set; }
        public string Description { get; set; }

        public int Stock { get; set; }

        public decimal Quantity { get; set; }

        public int Discount { get; set; }

        public SizeType SizeType { get; set; }

        public int Rating { get; set; }

        public int TotalRatings { get; set; }

        public string OtherDetails { get; set; }

        public int ReviewCount { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}

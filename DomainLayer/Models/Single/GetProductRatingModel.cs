using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryWala.DomainLayer.Models.Single
{
    public class GetProductRatingModel
    {
        public string UserId { get; set; }
        public string ProductId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryWala.DomainLayer.Entities
{
    public class OfferEntity
    {
        public int Id { get; set; }
        public string OfferCode { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int OffPrice { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
    }
}

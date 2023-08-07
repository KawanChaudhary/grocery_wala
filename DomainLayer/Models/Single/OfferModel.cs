using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryWala.DomainLayer.Models.Single
{
    public class OfferModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Offer Code is required.")]
        public string OfferCode { get; set; }
        [Required(ErrorMessage = "Offer description is required.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Offer Price is required.")]
        public int Price { get; set; }
        [Required(ErrorMessage = "Offer Off Price is required.")]
        public int OffPrice { get; set; }
        [Required(ErrorMessage = "Strat date is required.")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "End Date is required.")]
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
    }

}

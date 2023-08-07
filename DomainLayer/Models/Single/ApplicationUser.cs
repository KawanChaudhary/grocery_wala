using Microsoft.AspNetCore.Identity;

namespace GroceryWala.DomainLayer.Models.Single
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

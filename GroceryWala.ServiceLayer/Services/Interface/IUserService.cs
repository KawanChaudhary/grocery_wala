using GroceryWala.DomainLayer.Models.Multiple;
using GroceryWala.DomainLayer.Models.Single;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryWala.DataServiceLayer.Services.Interface
{
    public interface IUserService
    {
        string FirstName();
        string GetEmail();
        string GetUserId();
        bool IsAdmin();
        bool IsAuthenticated();
        string LastName();
        UserModel GetAllDetails();
        Task<int> AddToCart(CartItemModel cartItem);
        Task<List<CartModel>> GetCartItems(string userId);
        Task<CartItemModel> GetCartItem(int itemId);
        Task<bool> DeleteCartItem(int itemId);
        Task<bool> UpdateCartItem(CartItemModel cartItem);



    }
}

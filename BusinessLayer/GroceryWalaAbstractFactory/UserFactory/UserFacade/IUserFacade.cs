using GroceryWala.DomainLayer.Models.Multiple;
using GroceryWala.DomainLayer.Models.Single;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroceryWala.BusinessLayer.GroceryWalaAbstractFactory.UserFactory.UserFacade
{
    public interface IUserFacade
    {
        Task<int> AddComment(CommentModel comment);
        Task<int> AddRating(RatingModel rating);
        Task<int> AddToCart(CartItemModel model);
        Task<bool> DeleteCartItem(int itemId);
        Task<CartItemModel> GetCartItem(int itemId);
        Task<List<CartModel>> GetCartItems(string userId);
        Task<List<CommentModel>> GetCommentsByProductId(string productId);
        RatingModel GetRatingOfProductByuser(string productId, string userId);
        UserModel GetUserDetails();
        Task<bool> UpdateCartItem(CartItemModel item);
    }
}
using GroceryWala.DataServiceLayer.Services.Interface;
using GroceryWala.DomainLayer.Models.Multiple;
using GroceryWala.DomainLayer.Models.Single;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryWala.BusinessLayer.GroceryWalaAbstractFactory.UserFactory.UserFacade
{
    public class UserFacade : IUserFacade
    {
        private readonly IUserService userService;
        private readonly IProductService productService;

        public UserFacade(IUserService userService, IProductService productService)
        {
            this.userService = userService;
            this.productService = productService;
        }

        public UserModel GetUserDetails()
        {
            return userService.GetAllDetails();
        }

        public async Task<int> AddToCart(CartItemModel model)
        {
            return await userService.AddToCart(model);
        }

        public async Task<List<CartModel>> GetCartItems(string userId)
        {
            return await userService.GetCartItems(userId);
        }

        public async Task<CartItemModel> GetCartItem(int itemId)
        {
            return await userService.GetCartItem(itemId);
        }


        public async Task<bool> UpdateCartItem(CartItemModel item)
        {
            return await userService.UpdateCartItem(item);
        }

        public async Task<bool> DeleteCartItem(int itemId)
        {
            return await userService.DeleteCartItem(itemId);
        }

        public async Task<int> AddComment(CommentModel comment)
        {
            return await productService.AddComment(comment);
        }

        public async Task<List<CommentModel>> GetCommentsByProductId(string productId)
        {
            return await productService.GetCommentsByProductIId(productId);
        }

        public async Task<int> AddRating(RatingModel rating)
        {
            return await productService.AddRating(rating);
        }

        public RatingModel GetRatingOfProductByuser(string productId, string userId)
        {
            return productService.GetRatingOfProductByuser(productId, userId);
        }
    }
}

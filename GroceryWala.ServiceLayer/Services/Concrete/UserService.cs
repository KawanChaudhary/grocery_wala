using GroceryWala.DataAccessLayer.Repository.UnitOfWork;
using GroceryWala.DataServiceLayer.Services.Interface;
using GroceryWala.DomainLayer.Entities;
using GroceryWala.DomainLayer.Models.Multiple;
using GroceryWala.DomainLayer.Models.Single;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GroceryWala.DataServiceLayer.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork unitOfWork;
        private readonly IProductService productService;

        public UserService(IHttpContextAccessor httpContext,
            UserManager<ApplicationUser> userManager,
            IUnitOfWork unitOfWork, 
            IProductService productService)
        {
            _httpContext = httpContext;
            _userManager = userManager;
            this.unitOfWork = unitOfWork;
            this.productService = productService;
        }

        public UserModel GetAllDetails()
        {
            if (!IsAuthenticated()) { return null; }

            var user = new UserModel()
            {
                Email = GetEmail(),
                Id = GetUserId(),
                FirstName = FirstName(),
                LastName = LastName(),
                IsAdmin = IsAdmin(),
                PhoneNumber = GetPhoneNumber()
            };
            return user;
        }

        public bool IsAuthenticated()
        {
            return _httpContext.HttpContext.User.Identity.IsAuthenticated;
        }

        public string GetEmail()
        {
            return _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.Name);
        }

        public string GetUserId()
        {
            if (IsAuthenticated())
            {
                string email = GetEmail();
                var user = _userManager.FindByEmailAsync(email);
                return user.Result.Id;
            }
            return "";
        }

        public bool IsAdmin()
        {
            if (IsAuthenticated())
            {
                string email = GetEmail();
                var user = _userManager.FindByEmailAsync(email);
                var roles = _userManager.GetRolesAsync(user.Result);
                foreach (var role in roles.Result)
                {
                    if (role == "Admin") return true;
                }
            }
            return false;
        }

        public string FirstName()
        {
            if (IsAuthenticated())
            {
                string email = GetEmail();
                var user = _userManager.FindByEmailAsync(email);
                return user.Result.FirstName;
            }
            return "";
        }

        public string LastName()
        {
            if (IsAuthenticated())
            {
                string email = GetEmail();
                var user = _userManager.FindByEmailAsync(email);
                return user.Result.LastName;
            }
            return "";
        }

        public string GetPhoneNumber()
        {
            if (IsAuthenticated())
            {
                string email = GetEmail();
                var user = _userManager.FindByEmailAsync(email);
                return user.Result.PhoneNumber;
            }
            return "";
        }

        public async Task<int> AddToCart(CartItemModel cartItem)
        {
            var newCartitem = new CartItemEntity()
            {
                UserId = cartItem.UserId,
                ProductId = cartItem.ProductId,
                Quantity = cartItem.Quantity
            };

            var res = await unitOfWork.CartItemRepository.Add(newCartitem);
            await unitOfWork.CompleteAsync();
            return newCartitem.Id;
        }

        public async Task<List<CartModel>> GetCartItems(string userId)
        {
            Expression<Func<CartItemEntity, bool>> condition = entity => entity.UserId == userId;

            var allItems = await unitOfWork.CartItemRepository.Find(condition);
            var items = new List<CartModel>();

            if (allItems.Any())
            {
                foreach (var item in allItems)
                {
                    var cartItem = new CartItemModel()
                    {
                        Id = item.Id,
                        ProductId = item.ProductId,
                        UserId = item.UserId,
                        Quantity = item.Quantity
                    };

                    var details = await productService.GetProductById(int.Parse(item.ProductId));

                    var image = productService.GetProductImage(item.ProductId);

                    items.Add(new CartModel()
                    {
                        Item = cartItem,
                        Details = details,
                        Image = image
                    });

                }
            }
            return items;

        }

        public async Task<CartItemModel> GetCartItem(int itemId)
        {

            var cartItem = await unitOfWork.CartItemRepository.GetById(itemId);


            if (cartItem == null)
            {
                throw new Exception("Item does not exist");
            }
            var item = new CartItemModel()
            {
                Id = cartItem.Id,
                ProductId = cartItem.ProductId,
                UserId = cartItem.UserId,
                Quantity = cartItem.Quantity
            };
            return item;
        }

        public async Task<bool> DeleteCartItem(int itemId)
        {
            var res = await unitOfWork.CartItemRepository.Delete(itemId);
            await unitOfWork.CompleteAsync();
            return res;
        }

        public async Task<bool> UpdateCartItem(CartItemModel cartItem)
        {

            var item = new CartItemEntity()
            {
                Id = cartItem.Id,
                ProductId = cartItem.ProductId,
                UserId = cartItem.UserId,
                Quantity = cartItem.Quantity
            };

            var res = unitOfWork.CartItemRepository.Update(item);
            await unitOfWork.CompleteAsync();
            return res;
        }

    }
}

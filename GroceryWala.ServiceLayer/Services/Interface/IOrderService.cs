using GroceryWala.DomainLayer.Models.Single;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroceryWala.DataServiceLayer.Services.Interface
{
    public interface IOrderService
    {
        Task<List<UserOrderModel>> GetAllUserOrder(string userId);
        UserOrderModel GetUserOrder(string userId, int orderId);
        Task<List<OrderModel>> GetOrderProductDetaiils(int orderId);
        Task<bool> RegisterOrderProductDetails(OrderModel orderModel);
        Task<UserOrderModel> RegisterUserOrder(UserOrderModel userOrderModel);

        Task<List<UserOrderModel>> GetAllOrder();

        Task<List<MostOrderProductModel>> GetMostOrderProducts(int month);
    }
}
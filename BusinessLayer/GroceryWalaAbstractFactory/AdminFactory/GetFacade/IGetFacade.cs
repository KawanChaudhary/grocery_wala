using GroceryWala.DomainLayer.Models.Multiple;
using GroceryWala.DomainLayer.Models.Single;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroceryWala.BusinessLayer.GroceryWalaAbstractFactory.AdminFactory.GetFacade
{
    public interface IGetFacade
    {
        Task<List<UserOrderModel>> GetAllOrders();
        Task<List<AllProductModel>> GetAllProducts();
        Task<List<MostOrderProductModel>> GetMostOrderProducts(int month);
    }
}
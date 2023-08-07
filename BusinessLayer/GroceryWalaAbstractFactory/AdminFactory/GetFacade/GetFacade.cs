using GroceryWala.DataServiceLayer.Services.Interface;
using GroceryWala.DomainLayer.Models.Multiple;
using GroceryWala.DomainLayer.Models.Single;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryWala.BusinessLayer.GroceryWalaAbstractFactory.AdminFactory.GetFacade
{
    public class GetFacade : IGetFacade
    {
        private readonly IProductService productService;
        private readonly IOrderService orderService;

        public GetFacade(IProductService productService, IOrderService orderService)
        {
            this.productService = productService;
            this.orderService = orderService;
        }

        public async Task<List<AllProductModel>> GetAllProducts()
        {
            var products = await productService.GetAllProductsDetails();

            var res = new List<AllProductModel>();

            foreach (var product in products)
            {
                string productId = product.Id.ToString();

                var productImage = productService.GetProductImage(productId);

                res.Add(new AllProductModel()
                {
                    Details = product,
                    Images = productImage,
                    IsInCart = false
                });
            }
            return res;
        }

        public async Task<List<UserOrderModel>> GetAllOrders()
        {
            return await orderService.GetAllOrder();
        }

        public async Task<List<MostOrderProductModel>> GetMostOrderProducts(int month)
        {
            return await orderService.GetMostOrderProducts(month);
        }
    }
}

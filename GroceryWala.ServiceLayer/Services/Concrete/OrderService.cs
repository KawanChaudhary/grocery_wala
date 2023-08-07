using GroceryWala.DataAccessLayer.Repository.UnitOfWork;
using GroceryWala.DataServiceLayer.Services.Interface;
using GroceryWala.DomainLayer.Entities;
using GroceryWala.DomainLayer.Models.Single;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GroceryWala.DataServiceLayer.Services.Concrete
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IProductService productService;

        public OrderService(IUnitOfWork unitOfWork, IProductService productService)
        {
            this.unitOfWork = unitOfWork;
            this.productService = productService;
        }

        // methdos fo user Order details

        public async Task<UserOrderModel> RegisterUserOrder(UserOrderModel userOrderModel)
        {

            var userOrderEntity = new UserOrderEntity()
            {
                UserId = userOrderModel.UserId,
                TotalMRP = userOrderModel.TotalMRP,
                DiscountOnMRP = userOrderModel.DiscountOnMRP,
                CouponCode = userOrderModel.CouponCode,
                ExtraDiscount = userOrderModel.ExtraDiscount,
                FinalAmount = userOrderModel.FinalAmount,
                CreatedAt = DateTime.Now,
            };

            var res = await unitOfWork.UserOrderRepository.Add(userOrderEntity);
            await unitOfWork.CompleteAsync();
            userOrderModel.OrderId = userOrderEntity.Id;
            return userOrderModel;
        }

        public UserOrderModel GetUserOrder(string userId, int orderId)
        {
            Expression<Func<UserOrderEntity, bool>> condition = entity =>
            entity.UserId == userId && entity.Id == orderId;

            var userOrder = unitOfWork.UserOrderRepository.FindFirst(condition);

            var res = new UserOrderModel()
            {
                OrderId = userOrder.Id,
                UserId = userOrder.UserId,
                TotalMRP = userOrder.TotalMRP,
                DiscountOnMRP = userOrder.DiscountOnMRP,
                CouponCode = userOrder.CouponCode,
                ExtraDiscount = userOrder.ExtraDiscount,
                FinalAmount = userOrder.FinalAmount,
                CreatedAt = userOrder.CreatedAt
            };
            return res;
        }


        public async Task<List<UserOrderModel>> GetAllUserOrder(string userId)
        {
            Expression<Func<UserOrderEntity, bool>> condition = entity => entity.UserId == userId;

            var alluserOrders = await unitOfWork.UserOrderRepository.Find(condition);

            var res = new List<UserOrderModel>();

            if (alluserOrders.Any())
            {
                foreach (var userOrder in alluserOrders)
                {
                    res.Add(new UserOrderModel()
                    {
                        OrderId = userOrder.Id,
                        UserId = userOrder.UserId,
                        TotalMRP = userOrder.TotalMRP,
                        DiscountOnMRP = userOrder.DiscountOnMRP,
                        CouponCode = userOrder.CouponCode,
                        ExtraDiscount = userOrder.ExtraDiscount,
                        FinalAmount = userOrder.FinalAmount,
                        CreatedAt = userOrder.CreatedAt
                    });
                }
            }
            return res;
        }


        // methods for Order Details of a particular product

        public async Task<bool> RegisterOrderProductDetails(OrderModel orderModel)
        {
            var orderEntity = new OrderEntity()
            {
                OrderId = orderModel.OrderId,
                UserId = orderModel.UserId,
                ProductId = orderModel.ProductId,
                Name = orderModel.Name,
                Price = orderModel.Price,
                ProductQuantity = orderModel.ProductQuantity,
                Discount = orderModel.Discount,
            };

            var res = await unitOfWork.OrderRepository.Add(orderEntity);
            await unitOfWork.CompleteAsync();
            return res;
        }

        public async Task<List<OrderModel>> GetOrderProductDetaiils(int orderId)
        {
            Expression<Func<OrderEntity, bool>> condition = entity => entity.OrderId == orderId;

            var allOrders = await unitOfWork.OrderRepository.Find(condition);

            var res = new List<OrderModel>();

            if (allOrders.Any())
            {
                foreach (var order in allOrders)
                {
                    var imageAddress = productService.GetProductImage(order.ProductId.ToString());
                    res.Add(new OrderModel()
                    {
                        OrderId = order.Id,
                        UserId = order.UserId,
                        ProductId = order.ProductId,
                        Name = order.Name,
                        ProductQuantity = order.ProductQuantity,
                        Price = order.Price,
                        Discount = order.Discount,
                        ImageAddress = imageAddress.ImageAddress
                    });
                }
            }
            return res;
        }

        public async Task<List<UserOrderModel>> GetAllOrder()
        {
            var allUserOrders = await unitOfWork.UserOrderRepository.All();

            var userOrders = new List<UserOrderModel>();

            if (allUserOrders.Any())
            {
                foreach(var order in allUserOrders)
                {
                    userOrders.Add(new UserOrderModel()
                    {
                        OrderId = order.Id,
                        UserId = order.UserId,
                        TotalMRP = order.TotalMRP,
                        DiscountOnMRP = order.DiscountOnMRP,
                        FinalAmount = order.FinalAmount,
                        CouponCode = order.CouponCode,
                        ExtraDiscount = order.ExtraDiscount,
                        CreatedAt = order.CreatedAt,
                    });
                }
            }
            return userOrders;
        }

        public async Task<List<MostOrderProductModel>> GetMostOrderProducts(int month)
        {
            Expression<Func<UserOrderEntity, bool>> condition1 = entity => entity.CreatedAt.Month == month;

            var allOrders = await unitOfWork.UserOrderRepository.Find(condition1);
            

            Dictionary<int, int> productData = new Dictionary<int, int>();

            if (allOrders.Any())
            {
                foreach(var order in allOrders)
                {
                    Expression<Func<OrderEntity, bool>> condition = entity => entity.OrderId == order.Id;
                    var productOrders = await unitOfWork.OrderRepository.Find(condition);

                    if (productOrders.Any())
                    {
                        foreach(var product in productOrders)
                        {
                            if (productData.ContainsKey(product.ProductId))
                            {
                                productData[product.ProductId] += product.ProductQuantity;
                            }
                            else
                            {
                                productData.Add(product.ProductId, product.ProductQuantity);
                            }
                        }
                    }

                }
            }

            var mostOrders = new List<MostOrderProductModel>();

            if (productData.Any())
            {
                foreach(var pair in productData)
                {
                    var product = await productService.GetProductById(pair.Key);
                    var imageAddress = productService.GetProductImage(pair.Key.ToString());

                    mostOrders.Add(new MostOrderProductModel()
                    {
                        Product = product,
                        Quantity = pair.Value,
                        ImageAddress = imageAddress.ImageAddress
                    });
                }
            }
            return mostOrders;
        }

    }
}

using GroceryWala.DataServiceLayer.Services.Interface;
using GroceryWala.DomainLayer.Models.Single;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GroceryWala.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    [Produces("application/json")]
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        public IActionResult Index()
        {
            return Ok("Success from order");
        }

        // Calls for user Order Details

        [HttpPost("adduserorder")]
        public async Task<IActionResult> AddUserOrder(UserOrderModel userOrderModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userOrder = await orderService.RegisterUserOrder(userOrderModel);

                    if (userOrder.OrderId > 0)
                    {
                        return Ok(new
                        {
                            response = true,
                            order = userOrder
                        });
                    }
                }
                return BadRequest(new
                {
                    error = ModelState.Values
                });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [HttpGet("getuserorder/{userId}/{orderId}")]
        public IActionResult GetUserOrder(string userId, int orderId)
        {
            try
            {
                var userOrder = orderService.GetUserOrder(userId, orderId);

                if(userOrder != null)
                {
                    return Ok(new
                    {
                        response = true,
                        order = userOrder
                    });
                }
                return BadRequest(new
                {
                    response = false
                });
            }
            catch(Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [HttpGet("getalluserorder/{userId}")]
        public async Task<IActionResult> GetAllUserOrder(string userId)
        {
            try
            {
                var res = await orderService.GetAllUserOrder(userId);

                return Ok(new
                {
                    response = true,
                    orders = res
                });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }




        // Calls for Order Details for a product

        [HttpPost("addproductorder")]
        public async Task<IActionResult> AddOrderForProduct(OrderModel orderModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var res = await orderService.RegisterOrderProductDetails(orderModel);

                    if (res)
                    {
                        return Ok(new
                        {
                            response = true
                        });
                    }
                }
                return BadRequest(new
                {
                    error = ModelState.Values
                });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [HttpGet("getorderproductdetails/{orderId}")]
        public async Task<IActionResult> GetOrderProductDetails(int orderId)
        {
            try
            {
                var res = await orderService.GetOrderProductDetaiils(orderId);

                return Ok(new
                {
                    response = true,
                    productOrders = res
                });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
    }
}

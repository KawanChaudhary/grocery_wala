using GroceryWala.BusinessLayer.GroceryWalaAbstractFactory;
using GroceryWala.DataServiceLayer.Services.Interface;
using GroceryWala.DomainLayer.Models.Multiple;
using GroceryWala.DomainLayer.Models.Single;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryWala.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    [Produces("application/json")]
    public class AdminController : Controller
    {
        private readonly IGroceryWalaAbstractFactory groceryWalaAbstractFactory;

        public AdminController(IGroceryWalaAbstractFactory groceryWalaAbstractFactory)
        {
            this.groceryWalaAbstractFactory = groceryWalaAbstractFactory;
        }

        public IActionResult Index()
        {
            return Ok("Success From admin");
        }

        [HttpPost("addnewproduct")]

        public async Task<IActionResult> AddNewProduct(ProductModel product)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    int productId = await groceryWalaAbstractFactory.AdminFactory.AddFacade.AddNewProduct(product);

                    return Ok(new
                    {
                        id = productId
                    });
                }
                return BadRequest(new
                {
                    error = ModelState.Values
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpPost("addproductimages/{productId}")]

        public async Task<IActionResult> AddProductImages(IFormFile[] images, string productId)
         {
            try
            {
                await groceryWalaAbstractFactory.AdminFactory.AddFacade.AddImages(images, productId);

                return Ok(new
                {
                    response = true
                });


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("allproducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var res = await groceryWalaAbstractFactory.AdminFactory.GetFacade.GetAllProducts();

                return Ok(new
                {
                    response = res
                });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost("addoffer")]
        public async Task<IActionResult> AddOffer(OfferModel offer)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    var offerId = await groceryWalaAbstractFactory.AdminFactory.AddFacade.AddOffers(offer);

                    if (offerId > 0)
                    {
                        return Ok(new
                        {
                            response = true,
                            offerId = offerId
                        });
                    }

                }
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState.Values);
            }
            catch (Exception Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Ex.Message);
            }
        }

        [HttpPut("updateproduct")]
        public async Task<IActionResult> UpdateProduct(ProductModel product)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    var productId = await groceryWalaAbstractFactory.AdminFactory.EditFacade.UpdateProduct(product);

                    if (productId > 0)
                    {
                        return Ok(new
                        {
                            response = true,
                            id = productId
                        });
                    }

                }
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState.Values);
            }
            catch (Exception Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Ex.Message);
            }
        }

        [HttpDelete("deleteimage/{imageId}")]
        public async Task<IActionResult> DeleteImageById(int imageId)
        {
            try
            {
                await groceryWalaAbstractFactory.AdminFactory.EditFacade.DeleteImageById(imageId);

                return Ok(new
                {
                    response = true
                });
            }
            catch(Exception Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Ex.Message);
            }
        }

        [HttpDelete("deleteproduct/{productId}")]
        public async Task<IActionResult> DeleteProductById(int productId)
        {
            try
            {
                await groceryWalaAbstractFactory.AdminFactory.EditFacade.DeleteProductById(productId);
                return Ok(new
                {
                    response = true
                });
            }
            catch (Exception Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Ex.Message);
            }
        }

        [HttpGet("allorders")]
        public async Task<IActionResult> GetAllOrders()
        {
            try{

                var orders = await groceryWalaAbstractFactory.AdminFactory.GetFacade.GetAllOrders();

                return Ok(new
                {
                    response = true,
                    orders = orders
                });

            }
            catch(Exception Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Ex.Message);
            }
        }
        
        [HttpGet("getmostorderproducts/{month}")]
        public async Task<IActionResult> GetMostOrderProducts(int month)
        {
            try
            {
                var products = await groceryWalaAbstractFactory.AdminFactory.
                    GetFacade.GetMostOrderProducts(month);

                return Ok(new
                {
                    response = true,
                    products = products
                });

            }
            catch(Exception Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Ex.Message);
            }
        }

    }
}

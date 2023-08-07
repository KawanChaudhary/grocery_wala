using GroceryWala.DataServiceLayer.Services.Interface;
using GroceryWala.DomainLayer.Models.Multiple;
using GroceryWala.DomainLayer.Models.Single;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryWala.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    public class HomeController : Controller
    {
        private readonly IProductService productService;

        public HomeController(IProductService productService)
        {
            this.productService = productService;
        }

        public IActionResult Index()
        {
            return Ok("Success From Home");
        }

        [HttpGet("category/{category}")]
        public async Task<IActionResult> GetProductsByCategory(int category)
        {
            try
            {
                var products = await productService.GetProductsByCategory(category);


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

                return Ok(new
                {
                    response = res
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("product/{productid}")]
        public async Task<IActionResult> GetProductById(int productid)
        {
            try
            {
                ProductModel product = await productService.GetProductById(productid);

                var images = await productService.GetAllProductsImages();

                var resImages = new List<ImageModel>();

                string productId = product.Id.ToString();

                foreach (var image in images)
                {
                    if (productId == image.ProductId)
                    {
                        resImages.Add(new ImageModel()
                        {
                            Id = image.Id,
                            ProductId = image.ProductId,
                            ImageAddress = "http://127.0.0.1:8080/images/" + image.ImageAddress.Substring(97)
                        });
                    }

                }

                return Ok(new
                {
                    product = product,
                    images = resImages
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("getoffers")]
        public async Task<IActionResult> GetOffers()
        {
            try
            {

                var offers = await productService.GetAllOffers();

                if (offers.Any())
                {
                    return Ok(new
                    {
                        response = true,
                        offers = offers
                    });
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

    }
}

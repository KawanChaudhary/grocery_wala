using GroceryWala.BusinessLayer.GroceryWalaAbstractFactory;
using GroceryWala.DataServiceLayer.Services.Interface;
using GroceryWala.DomainLayer.Models.Single;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryWala.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    [Produces("application/json")]
    public class UserController : Controller
    {
        private readonly IGroceryWalaAbstractFactory groceryWalaAbstractFactory;

        public UserController(IGroceryWalaAbstractFactory groceryWalaAbstractFactory)
        {
            this.groceryWalaAbstractFactory = groceryWalaAbstractFactory;
        }
        public IActionResult Index()
        {
            return Ok("Success From User");
        }


        [HttpGet("getuserdetails")]
        public IActionResult GetUserDetails()
        {
            try
            {
                var user = groceryWalaAbstractFactory.UserFactory.UserFacade.GetUserDetails();
                if (user == null)
                {
                    return Ok(new
                    {
                        response = false
                    });
                }
                return Ok(new
                {
                    response = true,
                    user = user
                });
            }
            catch (Exception Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Ex.Message);
            }
        }

        [HttpPost("addtocart")]
        public async Task<IActionResult> AddToCart(CartItemModel cartItem)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var itemId = await groceryWalaAbstractFactory.UserFactory.UserFacade.AddToCart(cartItem);

                    if (itemId > 0)
                    {
                        return Ok(new
                        {
                            response = true,
                            itemId = itemId
                        });
                    }
                }
                throw new Exception("Something went wrong.");

            }
            catch (Exception Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Ex.Message);
            }
        }

        [HttpGet("getcartitems/{userId}")]
        public async Task<IActionResult> GetCartItems(string userId)
        {
            try
            {

                var items = await groceryWalaAbstractFactory.UserFactory.UserFacade.GetCartItems(userId);

                if (items.Any())
                {
                    return Ok(new
                    {
                        response = true,
                        items = items
                    });
                }
                return Ok(new
                {
                    response = false
                });

            }
            catch (Exception Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Ex.Message);
            }
        }

        [HttpGet("getcartitem/{itemId}")]
        public async Task<IActionResult> GetCartItem(int itemId)
        {
            try
            {

                var item = await groceryWalaAbstractFactory.UserFactory.UserFacade.GetCartItem(itemId);

                if (item != null)
                {
                    return Ok(new
                    {
                        response = true,
                        item = item
                    });
                }
                return Ok(new
                {
                    response = false
                });

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [HttpPut("updatecartitem")]
        public async Task<IActionResult> UpdateCartItem(CartItemModel cartItem)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var item = await groceryWalaAbstractFactory.UserFactory.UserFacade.UpdateCartItem(cartItem);

                    if (item == true)
                    {
                        return Ok(new
                        {
                            response = true
                        });
                    }
                }
                return Ok(new
                {
                    response = false
                });

            }
            catch (Exception Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Ex.Message);
            }
        }

        [HttpDelete("deletecartitem/{itemId}")]
        public async Task<IActionResult> DeleteCartItem(int itemId)
        {
            try
            {

                var item = await groceryWalaAbstractFactory.UserFactory.UserFacade.DeleteCartItem(itemId);

                if (item == true)
                {
                    return Ok(new
                    {
                        response = true
                    });
                }
                return Ok(new
                {
                    response = false
                });

            }
            catch (Exception Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Ex.Message);
            }
        }

        [HttpPost("addcomment")]
        public async Task<IActionResult> AddComment(CommentModel comment)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var commentId = await groceryWalaAbstractFactory.UserFactory.UserFacade.AddComment(comment);

                    if (commentId > 0)
                    {
                        return Ok(new
                        {
                            response = true,
                            commentId = commentId
                        });
                    }
                }
                throw new Exception("Something went wrong. Please try again");

            }
            catch (Exception Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Ex.Message);
            }
        }

        [HttpGet("getcommentsbyproduct/{productId}")]
        public async Task<IActionResult> GetCommentsByProduct(string productId)
        {
            try
            {

                var comments = await groceryWalaAbstractFactory.UserFactory.UserFacade.GetCommentsByProductId(productId);

                return Ok(new
                {
                    response = true,
                    comments = comments
                });
            }

            catch (Exception Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Ex.Message);
            }
        }


        [HttpPost("addrating")]
        public async Task<IActionResult> AddRating(RatingModel rating)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var ratingId = await groceryWalaAbstractFactory.UserFactory.UserFacade.AddRating(rating);
                    if (ratingId > 0)
                    {
                        return Ok(new
                        {
                            response = true,
                            ratingId = ratingId
                        });
                    }
                }
                throw new Exception("Something went wrong. Please try again");
            }
            catch (Exception Ex) { 
                return StatusCode(StatusCodes.Status500InternalServerError, Ex.Message); 
            }

        }

        [HttpGet("getratingofproduct/{productId}/{userId}")]
        public IActionResult GetRatingOfProductByuser(string productId, string userId)
        {
            try
            {

                var rating = groceryWalaAbstractFactory.UserFactory.UserFacade.GetRatingOfProductByuser(productId, userId);

                if (rating is null)
                {
                    return Ok(new
                    {
                        response = false
                    });
                }

                return Ok(new
                {
                    response = true,
                    rating = rating
                });
            }

            catch (Exception Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Ex.Message);
            }
        }

        [HttpGet("getreviewofproduct/{productId}/{userId}")]
        public async Task<IActionResult> GetReviewRating(string productId, string userId)
        {
            try
            {

                var rating = groceryWalaAbstractFactory.UserFactory.UserFacade.GetRatingOfProductByuser(productId, userId);
                var comments = await groceryWalaAbstractFactory.UserFactory.UserFacade.GetCommentsByProductId(productId);

                if (rating is null)
                {
                    return Ok(new
                    {
                        response = false
                    });
                }

                return Ok(new
                {
                    response = true,
                    comments = comments,
                    rating = rating
                });
            }

            catch (Exception Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Ex.Message);
            }
        }
    }
}

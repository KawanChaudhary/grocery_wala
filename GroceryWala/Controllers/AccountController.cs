using GroceryWala.DataServiceLayer.Services.Interface;
using GroceryWala.DomainLayer.Models.Single;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GroceryWala.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    [Produces("application/json")]
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public IActionResult Index()
        {
            return Ok("Success From Account");
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUpUser(SignUpUserModel user)
        {
            try
            {

                var result = await accountService.SignUp(user);
                if (!result.Succeeded)
                {
                    var msg = "";
                    foreach (var errorMessage in result.Errors)
                    {
                        msg = errorMessage.Description;
                        break;
                    }
                    return BadRequest(new
                    {
                        error = msg
                    });
                }
                ModelState.Clear();
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

        [HttpPost("signin")]
        public async Task<IActionResult> SignInUser(SignInUserModel userModel)
        {
            try
            {
                var res = await accountService.SignIn(userModel);
                if (res != null)
                {
                    return Ok(new
                    {
                        token = res.Token,
                        expire = res.Expire
                    });
                }
                else
                {
                    throw new Exception("Invalid Credentials");
                }

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message); ;
            }
        }


        [HttpGet("signout")]
        public async Task<IActionResult> SignOutuser()
        {
            try
            {
                await accountService.SignOut();

                return Ok(new
                {
                    response = true,
                });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message); ;
            }
        }

    }
}

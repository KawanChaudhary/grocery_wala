using GroceryWala.DomainLayer.Models.Single;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GroceryWala.DataServiceLayer.Services.Interface
{
    public interface IAccountService
    {
        Task<IdentityResult> SignUp(SignUpUserModel userModel);
        Task<TokenViewModel> SignIn(SignInUserModel userModel);
        Task SignOut();
    }
}
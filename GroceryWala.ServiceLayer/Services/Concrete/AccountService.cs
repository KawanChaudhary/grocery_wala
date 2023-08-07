using GroceryWala.DataServiceLayer.Services.Interface;
using GroceryWala.DomainLayer.Models.Single;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GroceryWala.DataServiceLayer.Services.Concrete
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;

        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this._userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this._configuration = configuration;
        }

        public async Task<IdentityResult> SignUp(SignUpUserModel userModel)
        {
            var user = new ApplicationUser()
            {
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Email = userModel.Email,
                UserName = userModel.Email,
                PhoneNumber = userModel.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);

            if (result.Succeeded)
            {
                var role = await roleManager.FindByNameAsync("User");

                if(role != null)
                {
                    await _userManager.AddToRoleAsync(user, role.Name);
                }
            }

            return result;
        }

        [Produces("application/json")]
        public async Task<TokenViewModel> SignIn(SignInUserModel userModel)
        {
            

            var user = await _userManager.FindByEmailAsync(userModel.Email);

            if (user == null) throw new Exception("User do not exist");

            var correctPassword = await _userManager.CheckPasswordAsync(user, userModel.Password);

            if (user != null && correctPassword)
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);

                var res = await signInManager.PasswordSignInAsync(userModel.Email, userModel.Password, false, false);

                if (res.Succeeded)
                {
                    return new TokenViewModel()
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        Expire = token.ValidTo
                    };

                }
            }
            else
            {
                throw new Exception("Invlaid Credentials");
            }
            return null;
        }

        public async Task SignOut()
        {
            await signInManager.SignOutAsync();
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}

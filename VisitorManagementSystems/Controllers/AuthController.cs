using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using VisitorManagementSystems.Constants;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using VisitorManagementSystems.Interfaces;

namespace VisitorManagementSystems.Controllers
{
    public class AuthController : Controller
    {
        #region Private Fields
        private readonly ILogger<AuthController> Logger;
        private readonly IUserProvider UserProvider;
        #endregion

        #region constructor
        public AuthController(ILogger<AuthController> logger, IUserProvider userProvider)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            UserProvider = userProvider;
        }
        #endregion

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Login(string userName,string password)
        {
            var user = await UserProvider.CheckUserPassword(userName, password);
            if(user != null)
            {
                var getuserRole = await UserProvider.GetUserRole(user.Id);
                if(getuserRole != null)
                {
                    return Ok(getuserRole); 
                }
                else
                {
                    return Unauthorized("No Role For This User");
                }
            }
            else
            {
                return Unauthorized("UserName or Password is invalid");
            }
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> SaveUser()
        {
            var name = Request.Form["username"].ToString();
            var password = Request.Form["password"].ToString();
            //Validate Credential 
            var login = await this.Login(name, password) as ObjectResult;
            if(login.StatusCode == 200)
            {
                var user = login.Value as Models.User;
                var claims = new[] { new Claim(ClaimTypes.Name, user.Name),
                                new Claim(UserAuthentication.CLAIM_UserId, user.Id.ToString()),
                                new Claim(UserAuthentication.CLAIM_UserRoleId, user.Role_Id.ToString()),
                                new Claim(UserAuthentication.CLAIM_UserRoleName,user.Role_Name)};

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(10),
                    IsPersistent = true,
                    IssuedUtc = DateTimeOffset.UtcNow,
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
                return Ok(new { status = "success", action = "redirect", location = "/home" });
            }
            else
            {
                return Unauthorized("Invalid username or password. Please try again.");
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                 CookieAuthenticationDefaults.AuthenticationScheme);
            Response.Redirect("/Home");
            return await Task.FromResult(View());
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VisitorManagementSystems.Enums;
using VisitorManagementSystems.Extentions;
using VisitorManagementSystems.Models;

namespace VisitorManagementSystems.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var role = User.Identity.GetUserRole();
                RoleEnum MyRole = (RoleEnum)Enum.Parse(typeof(RoleEnum), role, true);
                switch (MyRole)
                {
                    case (RoleEnum.Administrator):
                        Response.Redirect("/Administrator");
                        return await Task.FromResult(View());
                    case (RoleEnum.Manager):
                        Response.Redirect("/Manager");
                        return await Task.FromResult(View());
                    case (RoleEnum.Gatekeeper):
                        Response.Redirect("/Gatekeeper");
                        return await Task.FromResult(View());
                    default:
                        return View();
                }
            }
            else
            {
                return View("~/Views/Auth/Index.cshtml");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

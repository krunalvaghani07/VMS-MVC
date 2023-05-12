using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisitorManagementSystems.Interfaces;
using VisitorManagementSystems.Models;

namespace VisitorManagementSystems.Controllers
{
    public class AdministratorController : Controller
    {
        private readonly ILogger<AdministratorController> _logger;
        private readonly IUserProvider UserProvider;
        private readonly IGateProvider GateProvider;

        public AdministratorController(ILogger<AdministratorController> logger,
                                IUserProvider userProvider, IGateProvider gateProvider)
        {
            _logger = logger;
            UserProvider = userProvider;
            GateProvider = gateProvider;
        }
        public async Task<IActionResult> Index()
        {
            var gates = await GateProvider.GetGates();
            ViewData["Gates"] = new SelectList(gates.ToList(),"Id","Name");
            return View();
        }
        public async Task<IActionResult> GetUser(int id)
        {
            User user = new User();
            var roles = await UserProvider.GetRoles();
            ViewData["Roles"] = new SelectList(roles.ToList(), "Role_Id", "Role_Name");
            if (id != 0)
            {
                user = await UserProvider.GetUserById(id);
            }
            return PartialView("AddEditUserPartial", user);
        }
        public async Task<IActionResult> GetUserGate(int id)
        {
            User user = new User();
            var gates = await GateProvider.GetGates();
            ViewData["Gates"] = new SelectList(gates.ToList(), "Id", "Name");
            if (id != 0)
            {
                user = await GateProvider.GetUserGate(id);
            }
            return PartialView("MapUserGatePartial", user);
        }
        public async Task<JsonResult> GetUsers()
        {
            var users = await UserProvider.GetUsersist();
            return Json(new { data = users.ToList() });
        }
        public async Task<IActionResult> AddEditUser(User user)
        {
            try
            {
                var _user = await UserProvider.AddEditUser(user);
                if(_user == null)
                {
                    return UnprocessableEntity("Eror In Adding/Editing User");
                }
                else
                {
                    var addUserrole = await this.MapeUserRole(user.UserRoleId, _user.Id, user.Role_Id);
                    if (addUserrole == null)
                    {
                        return UnprocessableEntity("Eror In Adding/Editing UserRole");
                    }
                    return Ok(_user);
                }
            }
            catch(Exception ex)
            {
                if(user.Id == 0)
                {
                    return UnprocessableEntity("Eror In Adding User");
                }
                return UnprocessableEntity("Eror In Editing User");
            }
        }
        public async Task<IActionResult> DeleteUser(int userId)
        {
            try
            {
                var user = await UserProvider.DeleteUser(userId);
                if (user.isActive)
                {
                    return UnprocessableEntity("User is Not Deactivated");
                }
                return Ok("User Deactivated Successfully");
            }
            catch
            {
                return UnprocessableEntity("Eror In Delting This User");
            }
        }
        public async Task<IActionResult> MapeUserGate(int id, int userId, int gateId)
        {
            var user = await UserProvider.MapeUserGate(id, userId, gateId);
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return UnprocessableEntity("User and Gate is not mapped, try again");
            }
        }
        public async Task<IActionResult> MapeUserRole(int userRoleId, int userId, int roleId)
        {
            var user = await UserProvider.AddEditUserRole(userRoleId, userId, roleId);
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return UnprocessableEntity("User and role is not mapped, try again");
            }
        }
        public async Task<IActionResult> SaveUserGate(User user)
        {
            try
            {
                var mapuser = await this.MapeUserGate(user.UserGateId, user.Id, user.GateId) as OkObjectResult;
                if(mapuser.StatusCode == 200)
                {
                    return Ok();
                }
                else
                {
                    return UnprocessableEntity("Can not map user with gate");
                }
            }
            catch
            {
                return UnprocessableEntity("Erro occured");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisitorManagementSystems.Models;

namespace VisitorManagementSystems.Interfaces
{
   public interface IUserProvider
    {
        Task<User> CheckUserPassword(string userName, string password);
        Task<User> GetUserRole(int userId);
        Task<User> GetUserById(int userId);
        Task<IEnumerable<User>> GetUsersist();
        Task<User> AddEditUser(User user);
        Task<User> DeleteUser(int userId);
        Task<User> MapeUserGate(int id, int userId, int gateId);
        Task<IEnumerable<User>> GetRoles();
        Task<User> AddEditUserRole(int userRoleid, int userId, int roleId);
    }
}

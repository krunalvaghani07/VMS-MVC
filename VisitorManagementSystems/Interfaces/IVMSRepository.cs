using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisitorManagementSystems.Models;

namespace VisitorManagementSystems.Interfaces
{
    public interface IVMSRepository
    {
        Task<User> CheckUserPassword(string userName, string password);
        Task<User> GetUserRole(int userId);
        Task<IEnumerable<User>> GetUsersList();
        Task<User> GetUserById(int userId);
        Task<User> AddEditUser(User user);
        Task<User> DeleteUser(int userId);
        Task<Visitor> DeleteVisitor(int id);
        Task<User> MapeUserGate(int id, int userId, int gateId);
        Task<Visitor> AddEditVisitor(Visitor visitor);
        Task<IEnumerable<Visitor>> GetTodaysVisitors();
        Task<Visitor> GetVisitorById(int id);
        Task<IEnumerable<Visitor>> GetAllVisitors();
        Task<IEnumerable<Gate>> GetGates();
        Task<IEnumerable<User>> GetRoles();
        Task<User> GetUserGate(int userId);
        Task<User> AddEditUserRole(int userRoleid, int userId, int roleId);
        Task<Visitor> ExitVisitor(int id, DateTime exitTime);
    }
}

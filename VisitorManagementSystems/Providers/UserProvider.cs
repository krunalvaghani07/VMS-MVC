using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisitorManagementSystems.Interfaces;
using VisitorManagementSystems.Models;

namespace VisitorManagementSystems.Providers
{
    public class UserProvider : IUserProvider
    {
        #region Private Properties
        private ILogger<UserProvider> Logger { get; }
        private IVMSRepository VMSRepository { get; }

        #endregion

        #region Public Constructors
        public UserProvider(IVMSRepository visitorRepository, ILogger<UserProvider> logger)
        {
            VMSRepository = visitorRepository ?? throw new ArgumentNullException(nameof(visitorRepository));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        #endregion
        public async Task<User> CheckUserPassword(string userName, string password)
        {
            return await VMSRepository.CheckUserPassword(userName, password);
        }
        public async Task<User> GetUserRole(int userId)
        {
            return await VMSRepository.GetUserRole(userId);
        }
        public async Task<IEnumerable<User>> GetUsersist()
        {
            return await VMSRepository.GetUsersList();
        }
        public async Task<User> GetUserById(int userId)
        {
            return await VMSRepository.GetUserById(userId);
        }
        public async Task<User> AddEditUser(User user)
        {
            return await VMSRepository.AddEditUser(user);
        }
        public async Task<User> DeleteUser(int userId)
        {
            return await VMSRepository.DeleteUser(userId);
        }
        public async Task<User> MapeUserGate(int id, int userId, int gateId)
        {
            return await VMSRepository.MapeUserGate(id, userId, gateId);
        }
        public async Task<IEnumerable<User>> GetRoles()
        {
            return await VMSRepository.GetRoles();
        }
        public async Task<User> AddEditUserRole(int userRoleid, int userId, int roleId)
        {
            return await VMSRepository.AddEditUserRole(userRoleid, userId, roleId);
        }
    }
}

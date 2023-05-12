using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisitorManagementSystems.Models;

namespace VisitorManagementSystems.Interfaces
{
    public interface IGateProvider
    {
        Task<IEnumerable<Gate>> GetGates();
        Task<User> GetUserGate(int userId);
    }
}

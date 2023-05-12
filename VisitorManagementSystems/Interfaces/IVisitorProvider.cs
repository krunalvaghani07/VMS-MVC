using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisitorManagementSystems.Models;

namespace VisitorManagementSystems.Interfaces
{
    public interface IVisitorProvider
    {
        Task<Visitor> AddEditVisitor(Visitor visitor);
        Task<IEnumerable<Visitor>> GetTodaysVisitors();
        Task<IEnumerable<Visitor>> GetAllVisitors();
        Task<Visitor> GetVisitorById(int id);
        Task<Visitor> DeleteVisitor(int id);
        Task<Visitor> ExitVisitor(int id, DateTime exitTime);
    }
}

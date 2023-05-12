using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisitorManagementSystems.Interfaces;
using VisitorManagementSystems.Models;

namespace VisitorManagementSystems.Providers
{
    public class VisitorProvider : IVisitorProvider
    {
        #region Private Properties
        private ILogger<VisitorProvider> Logger { get; }
        private IVMSRepository VMSRepository { get; }

        #endregion

        #region Public Constructors
        public VisitorProvider(IVMSRepository visitorRepository, ILogger<VisitorProvider> logger)
        {
            VMSRepository = visitorRepository ?? throw new ArgumentNullException(nameof(visitorRepository));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        #endregion

        public async Task<Visitor> DeleteVisitor(int id)
        {
            return await VMSRepository.DeleteVisitor(id);
        }
        public async Task<Visitor> AddEditVisitor(Visitor visitor)
        {
            return await VMSRepository.AddEditVisitor(visitor);
        }
        public async Task<IEnumerable<Visitor>> GetTodaysVisitors()
        {
            return await VMSRepository.GetTodaysVisitors();
        }
        public async Task<IEnumerable<Visitor>> GetAllVisitors()
        {
            return await VMSRepository.GetAllVisitors();
        }
        public async Task<Visitor> GetVisitorById(int id)
        {
            return await VMSRepository.GetVisitorById(id);
        }
        public async Task<Visitor> ExitVisitor(int id, DateTime exitTime)
        {
            return await VMSRepository.ExitVisitor(id, exitTime);
        }
    }
}

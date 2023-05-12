using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisitorManagementSystems.Interfaces;
using VisitorManagementSystems.Models;

namespace VisitorManagementSystems.Providers
{
    public class GateProvider:IGateProvider
    {
        #region Private Properties
        private ILogger<GateProvider> Logger { get; }
        private IVMSRepository VMSRepository { get; }

        #endregion

        #region Public Constructors
        public GateProvider(IVMSRepository visitorRepository, ILogger<GateProvider> logger)
        {
            VMSRepository = visitorRepository ?? throw new ArgumentNullException(nameof(visitorRepository));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        #endregion
        public async Task<IEnumerable<Gate>> GetGates()
        {
            return await VMSRepository.GetGates();
        }
        public async Task<User> GetUserGate(int userId)
        {
            return await VMSRepository.GetUserGate(userId);
        }
    }
}

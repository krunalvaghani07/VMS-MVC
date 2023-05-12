using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisitorManagementSystems.Interfaces;
using VisitorManagementSystems.Providers;
using VisitorManagementSystems.Repositories;

namespace VisitorManagementSystems.Dependencies
{
    public static class ServicesDependency
    {
        public static void AddServiceDependency(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IUserProvider, UserProvider>();
            services.AddSingleton<IVisitorProvider, VisitorProvider>();
            services.AddSingleton<IGateProvider, GateProvider>();

            services.AddTransient<IVMSRepository, VMSRepository>();
            
        }
    }
}

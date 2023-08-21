using Microsoft.Extensions.DependencyInjection;
using PortailRH.BLL.Services.EmployeService;
using PortailRH.BLL.Services.TypeContratService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL
{
    /// <summary>
    /// DependencyInjection
    /// </summary>
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IEmployeService, EmployeService>();
            services.AddScoped<ITypeContratService, TypeContratService>();

            return services;
        }
    }
}

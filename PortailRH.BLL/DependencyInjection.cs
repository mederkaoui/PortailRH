using Microsoft.Extensions.DependencyInjection;
using PortailRH.BLL.Services.AbsenceService;
using PortailRH.BLL.Services.AnnonceService;
using PortailRH.BLL.Services.AuthentificationService;
using PortailRH.BLL.Services.CongeService;
using PortailRH.BLL.Services.DemandeDocumentService;
using PortailRH.BLL.Services.DepartementService;
using PortailRH.BLL.Services.EmployeService;
using PortailRH.BLL.Services.PaiementService;
using PortailRH.BLL.Services.PresenceService;
using PortailRH.BLL.Services.RecrutementService;
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
            services.AddScoped<IRecrutementService, RecrutementService>();
            services.AddScoped<IDepartementService, DepartementService>();
            services.AddScoped<ICongeService, CongeService>();
            services.AddScoped<IAbsenceService, AbsenceService>();
            services.AddScoped<IPresenceService, PresenceService>();
            services.AddScoped<IDemandeDocumentService, DemandeDocumentService>();
            services.AddScoped<IAnnonceService, AnnonceService>();
            services.AddScoped<IPaiementService, PaiementService>();
            services.AddScoped<IAuthentificationService, AuthentificationService>();

            return services;
        }
    }
}

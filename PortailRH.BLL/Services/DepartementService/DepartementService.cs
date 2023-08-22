using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PortailRH.BLL.Dtos.Employe;
using PortailRH.BLL.Dtos.Shared;
using PortailRH.DAL.Entities;
using PortailRH.DAL.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Services.DepartementService
{
    /// <summary>
    /// DepartementService
    /// </summary>
    public class DepartementService : IDepartementService
    {
        /// <summary>
        /// IGenericRepository<Departement>
        /// </summary>
        private readonly IGenericRepository<Departement> _departementRepository;

        /// <summary>
        /// ILogger<DepartementService>
        /// </summary>
        private readonly ILogger<DepartementService> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="departementRepository">IGenericRepository<Departement></param>
        /// <param name="logger">ILogger<DepartementService></param>
        public DepartementService(IGenericRepository<Departement> departementRepository, ILogger<DepartementService> logger)
        {
            _departementRepository = departementRepository;
            _logger = logger;
        }

        /// <summary>
        /// Get Departements with their fonctions
        /// </summary>
        /// <returns>ICollection<DepartementDto></returns>
        public async Task<ICollection<DepartementDto>> GetDepartemtnsWithFonction()
        {
            var departements = await _departementRepository.GetListAsync(include: inc => inc.Include(x => x.Fonctions));

            return departements.Select(x => new DepartementDto
            {
                Id = x.Id,
                Nom = x.Nom,
                Fonctions = x.Fonctions.Select(f => new FonctionDto
                {
                    Id = f.Id,
                    Nom = f.Nom
                }).ToList()
            }).ToList();
        }
    }
}

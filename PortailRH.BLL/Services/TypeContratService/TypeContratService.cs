using Microsoft.Extensions.Logging;
using PortailRH.BLL.Dtos.Employe;
using PortailRH.DAL.Entities;
using PortailRH.DAL.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Services.TypeContratService
{
    public class TypeContratService : ITypeContratService
    {
        /// <summary>
        /// IGenericRepository<TypeContrat>
        /// </summary>
        private readonly IGenericRepository<TypeContrat> _typeContratRepository;

        /// <summary>
        /// ILogger<TypeContratService>
        /// </summary>
        private readonly ILogger<TypeContratService> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="typeContratRepository">IGenericRepository<TypeContrat></param>
        /// <param name="logger">ILogger<TypeContratService></param>
        public TypeContratService(IGenericRepository<TypeContrat> typeContratRepository, ILogger<TypeContratService> logger)
        {
            _typeContratRepository = typeContratRepository;
            _logger = logger;
        }

        /// <summary>
        /// Get types of contracts
        /// </summary>
        /// <returns>ICollection<TypeContratDto></returns>
        public async Task<ICollection<TypeContratDto>> GetTypesContrat()
        {
            var typesContrat = await _typeContratRepository.GetListAsync();

            return typesContrat.Select(x => new TypeContratDto
            {
                Id = x.Id,
                Nom = x.Nom
            }).ToList();
        }
    }
}

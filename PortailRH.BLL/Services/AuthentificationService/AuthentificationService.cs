using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PortailRH.BLL.Dtos.Authentification;
using PortailRH.BLL.Dtos.Shared;
using PortailRH.DAL.Entities;
using PortailRH.DAL.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Services.AuthentificationService
{
    /// <summary>
    /// AuthentificationService
    /// </summary>
    public class AuthentificationService : IAuthentificationService
    {
        /// <summary>
        /// IGenericRepository<Authentification>
        /// </summary>
        private readonly IGenericRepository<Authentification> _authentificationRepository;

        /// <summary>
        /// ILogger<AuthentificationService>
        /// </summary>
        private readonly ILogger<AuthentificationService> _logger;

        /// <summary>
        /// HUMAN_RESOURCES_CONST
        /// </summary>
        private const string HUMAN_RESOURCES_CONST = "Ressource Humaine";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="authentificationRepository">IGenericRepository<Authentification></param>
        /// <param name="logger">ILogger<AuthentificationService></param>
        public AuthentificationService(IGenericRepository<Authentification> authentificationRepository, ILogger<AuthentificationService> logger)
        {
            _authentificationRepository = authentificationRepository;
            _logger = logger;
        }

        /// <summary>
        /// Check user information and return it owen type
        /// </summary>
        /// <param name="loginDto">LoginDto</param>
        /// <returns>AuthentificationTypeEnum</returns>
        public async Task<AuthentificationTypeEnum> Login(LoginDto loginDto)
        {
            var user = await _authentificationRepository.GetAsync(
                                                                predicate: x => x.NomUtilisateur == loginDto.Username &&
                                                                                x.MotDePasse == loginDto.Password,
                                                                include: inc => inc.Include(x => x.CinEmployeNavigation)
                                                                                    .ThenInclude(x => x.IdFonctionNavigation)
                                                                                    .ThenInclude(x => x.IdDepartementNavigation)
                                                            );
            _logger.LogInformation($"Get user loged with username: {loginDto.Username} and password: {loginDto.Password}");

            if (user == null)
            {
                return AuthentificationTypeEnum.PasUnUtilisateur;
            }

            if (user.CinEmployeNavigation?.IdFonctionNavigation?.IdDepartementNavigation?.Nom?.ToLower() == HUMAN_RESOURCES_CONST.ToLower())
            {
                return AuthentificationTypeEnum.Administrateur;
            }

            return AuthentificationTypeEnum.Employe;
        }
    }
}

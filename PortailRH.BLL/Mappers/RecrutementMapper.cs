using PortailRH.BLL.Dtos.Recrutement;
using PortailRH.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Mappers
{
    /// <summary>
    /// RecrutementMapper
    /// </summary>
    public static class RecrutementMapper
    {
        public static GetRecrutementDto ToGetRecrutementDto(this Recrutement recrutement)
        {
            return new GetRecrutementDto
            {
                Id = recrutement.Id,
                Nom = recrutement.Nom,
                Prenom = recrutement.Prenom,
                Email = recrutement.Email,
                Telephone = recrutement.Telephone,
                Departement = recrutement.IdFonctionNavigation.IdDepartementNavigation.Nom,
                Fonction = recrutement.IdFonctionNavigation.Nom,
                DateCreation = recrutement.DatedCreation,
                Document = recrutement.IdDocumentNavigation.Nom
            };
        }
    }
}

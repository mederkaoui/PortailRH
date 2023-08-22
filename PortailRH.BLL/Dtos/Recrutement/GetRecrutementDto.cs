using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Dtos.Recrutement
{
    /// <summary>
    /// GetRecrutementDto
    /// </summary>
    public class GetRecrutementDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Nom
        /// </summary>
        public string? Nom { get; set; }

        /// <summary>
        /// Prenom
        /// </summary>
        public string? Prenom { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Telephone
        /// </summary>
        public string? Telephone { get; set; }

        /// <summary>
        /// DateCreation
        /// </summary>
        public DateTime? DateCreation { get; set; }

        /// <summary>
        /// Departement
        /// </summary>
        public string? Departement { get; set; }

        /// <summary>
        /// Fonction
        /// </summary>
        public string? Fonction { get; set; }

        /// <summary>
        /// Document
        /// </summary>
        public string? Document { get; set; }
    }
}

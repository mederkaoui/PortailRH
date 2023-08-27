using PortailRH.BLL.Dtos.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Dtos.DemandeDocument
{
    /// <summary>
    /// DemandeDocumentDto
    /// </summary>
    public class DemandeDocumentDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// CIN
        /// </summary>
        public string? CIN { get; set; }

        /// <summary>
        /// Nom
        /// </summary>
        public string? Nom { get; set; }

        /// <summary>
        /// Prenom
        /// </summary>
        public string? Prenom { get; set; }

        /// <summary>
        /// TitreDocument
        /// </summary>
        public string? TitreDocument { get; set; }

        /// <summary>
        /// DateDemande
        /// </summary>
        public DateTime? DateDemande { get; set; }

        /// <summary>
        /// Raison
        /// </summary>
        public string? Raison { get; set; }

        /// <summary>
        /// DocumentNom
        /// </summary>
        public string? DocumentNom { get; set; }

        /// <summary>
        /// DemandeDocumentStatusEnum
        /// </summary>
        public DemandeDocumentStatusEnum? Status { get; set; }
    }
}

using PortailRH.BLL.Dtos.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Dtos.DemandeDocument
{
    /// <summary>
    /// DemandeDocumentSearchDto
    /// </summary>
    public class DemandeDocumentSearchDto
    {
        /// <summary>
        /// SearchQuery => CIN - Nom - Prenom
        /// </summary>
        public string? SearchQuery { get; set; }

        /// <summary>
        /// DemandeDocumentStatusEnum
        /// </summary>
        public DemandeDocumentStatusEnum? Statut { get; set; }

        /// <summary>
        /// DateDemande
        /// </summary>
        public DateTime? DateDemande { get; set; }

        /// <summary>
        /// CurrentPage
        /// </summary>
        public int CurrentPage { get; set; } = 1;

        /// <summary>
        /// ItemsPerPage
        /// </summary>
        public int ItemsPerPage { get; set; } = 10;
    }
}

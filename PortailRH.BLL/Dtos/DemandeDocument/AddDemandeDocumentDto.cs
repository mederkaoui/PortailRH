using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Dtos.DemandeDocument
{
    /// <summary>
    /// AddDemandeDocumentDto
    /// </summary>
    public class AddDemandeDocumentDto
    {
        /// <summary>
        /// CIN
        /// </summary>
        public string? CIN { get; set; }

        /// <summary>
        /// TitreDocument
        /// </summary>
        public required string TitreDocument { get; set; }

        /// <summary>
        /// DateDemande
        /// </summary>
        public required DateTime DateDemande { get; set; }

        /// <summary>
        /// Raison
        /// </summary>
        public string? Raison { get; set; }

        /// <summary>
        /// IdTypeDocument
        /// </summary>
        public required int IdTypeDocument { get; set; }

    }
}

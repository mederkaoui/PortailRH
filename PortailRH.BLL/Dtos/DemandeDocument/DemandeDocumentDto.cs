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
    public class DemandeDocumentDto : EmployeDemandeDocumentDto
    {
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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Dtos.DemandeDocument
{
    /// <summary>
    /// EmployeDemandeDocumentDto
    /// </summary>
    public class EmployeDemandeDocumentDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }

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
        /// TypeDocument
        /// </summary>
        public string? TypeDocument { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public string? Status { get; set; }
    }
}

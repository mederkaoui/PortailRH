using PortailRH.BLL.Dtos.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Dtos.DemandeDocument
{
    /// <summary>
    /// EmployeDemandeDocumentPaginatedListDto
    /// </summary>
    public class EmployeDemandeDocumentPaginatedListDto
    {
        /// <summary>
        /// ICollection<EmployeDemandeDocumentDto>
        /// </summary>
        public ICollection<EmployeDemandeDocumentDto>? Demandes { get; set; }

        /// <summary>
        /// PaginatedDto
        /// </summary>
        public PaginatedDto? Pagining { get; set; }
    }
}

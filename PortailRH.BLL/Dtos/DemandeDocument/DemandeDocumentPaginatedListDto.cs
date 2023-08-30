using PortailRH.BLL.Dtos.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Dtos.DemandeDocument
{
    /// <summary>
    /// DemandeDocumentPaginatedListDto
    /// </summary>
    public class DemandeDocumentPaginatedListDto
    {
        /// <summary>
        /// ICollection<DemandeDocumentDto>
        /// </summary>
        public ICollection<DemandeDocumentDto>? Demandes { get; set; }

        /// <summary>
        /// PaginatedDto
        /// </summary>
        public PaginatedDto? Pagining { get; set; }
    }
}

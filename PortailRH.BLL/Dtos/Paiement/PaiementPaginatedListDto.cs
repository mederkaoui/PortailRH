using PortailRH.BLL.Dtos.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Dtos.Paiement
{
    /// <summary>
    /// PaiementPaginatedListDto
    /// </summary>
    public class PaiementPaginatedListDto
    {
        /// <summary>
        /// ICollection<PaiementDto>
        /// </summary>
        public ICollection<PaiementDto>? Paiements { get; set; }

        /// <summary>
        /// PaginatedDto
        /// </summary>
        public PaginatedDto? Pagining { get; set; }
    }
}

using PortailRH.BLL.Dtos.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Dtos.Conge
{
    /// <summary>
    /// DemandesCongesPaginatedListDto
    /// </summary>
    public class DemandesCongesPaginatedListDto
    {
        /// <summary>
        /// ICollection<DemandesCongesDto>
        /// </summary>
        public ICollection<DemandesCongesDto>? Demandes { get; set; }

        /// <summary>
        /// PaginatedDto
        /// </summary>
        public PaginatedDto? Pagining { get; set; }
    }
}

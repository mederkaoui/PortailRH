using PortailRH.BLL.Dtos.Employe;
using PortailRH.BLL.Dtos.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Dtos.Recrutement
{
    /// <summary>
    /// RecrutementPaginatedListDto
    /// </summary>
    public class RecrutementPaginatedListDto
    {
        /// <summary>
        /// ICollection<GetRecrutementDto>
        /// </summary>
        public ICollection<GetRecrutementDto>? Recrutements { get; set; }

        /// <summary>
        /// PaginatedDto
        /// </summary>
        public PaginatedDto? Pagining { get; set; }
    }
}

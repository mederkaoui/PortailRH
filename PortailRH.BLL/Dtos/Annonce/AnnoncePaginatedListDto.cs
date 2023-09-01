using PortailRH.BLL.Dtos.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Dtos.Annonce
{
    /// <summary>
    /// AnnoncePaginatedListDto
    /// </summary>
    public class AnnoncePaginatedListDto
    {
        /// <summary>
        /// ICollection<AnnonceDto>
        /// </summary>
        public ICollection<AnnonceDto>? Annonces { get; set; }

        /// <summary>
        /// PaginatedDto
        /// </summary>
        public PaginatedDto? Pagining { get; set; }
    }
}

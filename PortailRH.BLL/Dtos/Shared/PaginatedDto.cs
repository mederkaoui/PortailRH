using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Dtos.Shared
{
    /// <summary>
    /// PaginatedDto
    /// </summary>
    public class PaginatedDto
    {
        /// <summary>
        /// CurrentPage
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// ItemsPerPage
        /// </summary>
        public int ItemsPerPage { get; set; }

        /// <summary>
        /// TotalItems
        /// </summary>
        public int TotalItems { get; set; }

        /// <summary>
        /// TotalPages
        /// </summary>
        public int TotalPages { get; set; }
    }
}

using PortailRH.BLL.Dtos.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Dtos.Employe
{
    /// <summary>
    /// EmployePaginatedListDto
    /// </summary>
    public class EmployePaginatedListDto
    {
        /// <summary>
        /// ICollection<GetEmployeDto>
        /// </summary>
        public ICollection<GetEmployeDto>? Employes { get; set; }

        /// <summary>
        /// PaginatedDto
        /// </summary>
        public PaginatedDto? Pagining { get; set; }
    }
}

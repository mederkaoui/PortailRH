using PortailRH.BLL.Dtos.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Dtos.Absence
{
    /// <summary>
    /// AbsencePaginatedListDto
    /// </summary>
    public class AbsencePaginatedListDto
    {
        /// <summary>
        /// ICollection<AbsenceDto>
        /// </summary>
        public ICollection<AbsenceDto>? Absences { get; set; }

        /// <summary>
        /// PaginatedDto
        /// </summary>
        public PaginatedDto? Pagining { get; set; }
    }
}

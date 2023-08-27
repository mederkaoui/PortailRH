using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Dtos.Absence
{
    /// <summary>
    /// AbsenceSearchDto
    /// </summary>
    public class AbsenceSearchDto
    {
        /// <summary>
        /// SearchQuery => CIN - Nom - Prenom
        /// </summary>
        public string? SearchQuery { get; set; }

        /// <summary>
        /// IdTypeAbsence
        /// </summary>
        public int? IdTypeAbsence { get; set; }

        /// <summary>
        /// CurrentPage
        /// </summary>
        public int CurrentPage { get; set; } = 1;

        /// <summary>
        /// ItemsPerPage
        /// </summary>
        public int ItemsPerPage { get; set; } = 10;
    }
}

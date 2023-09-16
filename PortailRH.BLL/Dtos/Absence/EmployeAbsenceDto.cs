using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Dtos.Absence
{
    /// <summary>
    /// EmployeAbsenceDto
    /// </summary>
    public class EmployeAbsenceDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// TypeAbsence
        /// </summary>
        public string? TypeAbsence { get; set; }

        /// <summary>
        /// DateDebut
        /// </summary>
        public DateTime? DateDebut { get; set; }

        /// <summary>
        /// DateFin
        /// </summary>
        public DateTime? DateFin { get; set; }

        /// <summary>
        /// Justifie
        /// </summary>
        public bool Justifie { get; set; }
    }
}

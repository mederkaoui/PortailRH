using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Dtos.Employe
{
    /// <summary>
    /// EmployeNouveauContrat
    /// </summary>
    public class EmployeNouveauContratDto
    {
        /// <summary>
        /// CIN
        /// </summary>
        public required string CIN { get; set; }

        /// <summary>
        /// TypeContract
        /// </summary>
        public required int TypeContrat { get; set; }

        /// <summary>
        /// DateEntree
        /// </summary>
        public required DateTime DateEntree { get; set; }

        /// <summary>
        /// Salaire
        /// </summary>
        public required float Salaire { get; set; }
    }
}

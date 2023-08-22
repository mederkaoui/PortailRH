using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Dtos.Recrutement
{
    /// <summary>
    /// RecrutementSearchDto
    /// </summary>
    public class RecrutementSearchDto
    {
        /// <summary>
        /// NomComplet
        /// </summary>
        public string? NomComplet { get; set; }

        /// <summary>
        /// Departement
        /// </summary>
        public int? Departement { get; set; }

        /// <summary>
        /// Fonction
        /// </summary>
        public int? Fonction { get; set; }

        /// <summary>
        /// DateDebut
        /// </summary>
        public DateTime? DateDebut { get; set; }

        /// <summary>
        /// DateFin
        /// </summary>
        public DateTime? DateFin { get; set; }

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

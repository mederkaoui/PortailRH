using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Dtos.Conge
{
    /// <summary>
    /// DemandesCongesDto
    /// </summary>
    public class DemandesCongesDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// DateDebut
        /// </summary>
        public DateTime? DateDebut { get; set; }

        /// <summary>
        /// DateFin
        /// </summary>
        public DateTime? DateFin { get; set; }

        /// <summary>
		/// Status
		/// </summary>
		public string? Status { get; set; }

        /// <summary>
        /// Nom
        /// </summary>
        public string? Nom { get; set; }

        /// <summary>
        /// Prenom
        /// </summary>
        public string? Prenom { get; set; }

        /// <summary>
        /// CIN
        /// </summary>
        public string? CIN { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Dtos.Presence
{
    /// <summary>
    /// PresenceDto
    /// </summary>
    public class PresenceDto
    {
        /// <summary>
        /// CIN
        /// </summary>
        public string? CIN { get; set; }

        /// <summary>
        /// Nom
        /// </summary>
        public string? Nom { get; set; }

        /// <summary>
        /// Prenom
        /// </summary>
        public string? Prenom { get; set; }

        /// <summary>
        /// Mois
        /// </summary>
        public int? Mois { get; set; }

        /// <summary>
        /// HeuresAbsence
        /// </summary>
        public int? HeuresAbsence { get; set; }
    }
}

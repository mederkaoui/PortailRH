using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Dtos.Presence
{
    /// <summary>
    /// PresenceSearchDto
    /// </summary>
    public class PresenceSearchDto
    {
        /// <summary>
        /// SearchQuery
        /// </summary>
        public string? SearchQuery { get; set; }

        /// <summary>
        /// Mois
        /// </summary>
        public int? Mois { get; set; } = DateTime.Now.Month;

        /// <summary>
        /// Annee
        /// </summary>
        public int? Annee { get; set; } = DateTime.Now.Year;
    }
}

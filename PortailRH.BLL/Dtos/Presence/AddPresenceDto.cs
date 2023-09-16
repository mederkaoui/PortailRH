using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Dtos.Presence
{
    /// <summary>
    /// AddPresenceDto
    /// </summary>
    public class AddPresenceDto
    {
        /// <summary>
        /// CIN
        /// </summary>
        public string? CIN { get; set; }

        /// <summary>
        /// DatePresence
        /// </summary>
        public DateTime? DatePresence { get; set; }

        /// <summary>
        /// HeuresTravaillees
        /// </summary>
        public int? HeuresTravaillees { get; set; }
    }
}

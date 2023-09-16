using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Dtos.Presence
{
    /// <summary>
    /// EmployePresenceDto
    /// </summary>
    public class EmployePresenceDto
    {
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

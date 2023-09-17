using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Dtos.Dashboard
{
    /// <summary>
    /// StatisticsDataDto
    /// </summary>
    public class StatisticsDataDto
    {
        /// <summary>
        /// Absences
        /// </summary>
        public int? Absences { get; set; } = 0;

        /// <summary>
        /// Paiements
        /// </summary>
        public double? Paiements { get; set; } = 0;

        /// <summary>
        /// TotalEmployes
        /// </summary>
        public int? TotalEmployes { get; set; } = 0;
    }
}

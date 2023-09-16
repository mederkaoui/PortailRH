using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Dtos.Dashboard
{
    /// <summary>
    /// EmployeChartDataDto
    /// </summary>
    public class EmployeChartDataDto
    {
        /// <summary>
        /// Year
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Month
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// EmployeeCount
        /// </summary>
        public int EmployeeCount { get; set; }
    }
}

using PortailRH.BLL.Dtos.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Services.DashboardService
{
    /// <summary>
    /// IDashboardService
    /// </summary>
    public interface IDashboardService
    {
        public Task<ICollection<EmployeChartDataDto>> GetChartData(int? year);
    }
}

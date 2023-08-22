using PortailRH.BLL.Dtos.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Services.DepartementService
{
    /// <summary>
    /// IDepartementService
    /// </summary>
    public interface IDepartementService
    {
        public Task<ICollection<DepartementDto>> GetDepartemtnsWithFonction();
    }
}

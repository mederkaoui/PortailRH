using PortailRH.BLL.Dtos.Presence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Services.PresenceService
{
    /// <summary>
    /// IPresenceService.cs
    /// </summary>
    public interface IPresenceService
    {
        public Task<ICollection<PresenceDto>> GetPresencesByMonths(PresenceSearchDto searchDto);
        public Task AddEmployePresence(AddPresenceDto presenceDto);
        public Task<ICollection<EmployePresenceDto>> GetEmployePresences(string cin);
    }
}

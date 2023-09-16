using PortailRH.BLL.Dtos.Absence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Services.AbsenceService
{
    /// <summary>
    /// IAbsenceService
    /// </summary>
    public interface IAbsenceService
    {
        public Task<AbsencePaginatedListDto> GetAbsencesPaginatedList(AbsenceSearchDto searchDto);
        public Task<ICollection<AbsenceTypeDto>> GetAbsenceTypes();
        public Task DeleteAbsence(int id);
        public Task AddAbsence(AddAbsenceDto absenceDto);
        public Task<AbsenceDto> GetDetailsAbsence(int id);
        public Task<ICollection<EmployeAbsenceDto>> GetEmployeAbsences(string cin);
        public Task UpdateAbsence(int id, EditAbsenceDto editAbsenceDto);
    }
}

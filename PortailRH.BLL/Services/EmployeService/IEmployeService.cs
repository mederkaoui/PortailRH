using PortailRH.BLL.Dtos.Employe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Services.EmployeService
{
    /// <summary>
    /// IEmployeService
    /// </summary>
    public interface IEmployeService
    {
        public Task<EmployeSelectListsDto> GetSelectListsData();

        public Task<EmployePaginatedListDto> GetEmployesPaginatedList(EmployeSearchDto searchDto);

        public Task AddedEmploye(NouvelEmployeDto employe);

        public  Task<DetailsEmployeDto> UpdateEmploye(NouvelEmployeDto employe);

        public Task DeleteEmploye(string cin);

        public Task DeleteEmployeDefinitely(string cin);

        public Task<DetailsEmployeDto> GetDetailsEmploye(string cin);

        public Task<EmployePaginatedListDto> GetAnciensEmployesPaginatedList(EmployeSearchDto searchDto);

        public Task NewContract(EmployeNouveauContratDto nouveauContratDto);
    }
}

using PortailRH.BLL.Dtos.Shared;
using PortailRH.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Dtos.Employe
{
    /// <summary>
    /// EmployeSelectListsDto
    /// </summary>
    public class EmployeSelectListsDto
    {
        /// <summary>
        /// ICollection<PaysDto>
        /// </summary>
        public ICollection<PaysDto>? Pays { get; set; }

        /// <summary>
        /// ICollection<BanqueDto>
        /// </summary>
        public ICollection<BanqueDto>? Banques { get; set; }

        /// <summary>
        /// ICollection<DepartementDto>
        /// </summary>
        public ICollection<DepartementDto>? Departements { get; set; }

        /// <summary>
        /// ICollection<TypeContrat>
        /// </summary>
        public ICollection<TypeContrat>? TypesContrat { get; set; }
    }
}

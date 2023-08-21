using PortailRH.BLL.Dtos.Employe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Services.TypeContratService
{
    /// <summary>
    /// ITypeContratService
    /// </summary>
    public interface ITypeContratService
    {
        public Task<ICollection<TypeContratDto>> GetTypesContrat();
    }
}

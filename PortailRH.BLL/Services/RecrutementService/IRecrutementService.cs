using PortailRH.BLL.Dtos.Recrutement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Services.RecrutementService
{
    /// <summary>
    /// RecrutementService
    /// </summary>
    public interface IRecrutementService
    {
        public Task<RecrutementPaginatedListDto> GetRecrutementPaginatedList(RecrutementSearchDto searchDto);

        public Task DeleteRecrutement(int id);

        public Task<GetRecrutementDto> GetDetialsRecrutement(int id);

        public Task AddNewRecrutement(NouveauxRecrutementDto recrutementDto);
    }
}

using PortailRH.BLL.Dtos.Annonce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Services.AnnonceService
{
    /// <summary>
    /// IAnnonceService
    /// </summary>
    public interface IAnnonceService
    {
        public Task<AnnoncePaginatedListDto> GetAnnonce(AnnonceSearchDto searchDto);
        public Task DeleteAnnonce(int id);
        public Task AddAnnonce(AnnonceDto annonce);
    }
}

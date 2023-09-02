using PortailRH.BLL.Dtos.Paiement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Services.PaiementService
{
    /// <summary>
    /// IPaiementService
    /// </summary>
    public interface IPaiementService
    {
        public Task<PaiementPaginatedListDto> GetPayments(PaiementSearchDto searchDto);
        public Task AddNewPayment(int month, int year);
    }
}

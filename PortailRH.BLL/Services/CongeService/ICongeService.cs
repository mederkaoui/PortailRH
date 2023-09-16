using PortailRH.BLL.Dtos.Conge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Services.CongeService
{
	/// <summary>
	/// ICongeService
	/// </summary>
	public interface ICongeService
	{
		public Task<ICollection<CongeDto>> GetConges();
		public Task<ICollection<CongeDto>> GetEmployeConges(string cin);
        public Task<DemandesCongesPaginatedListDto> GetAllConges(DemandesSearchDto searchDto);
        public Task<DemandesCongesPaginatedListDto> GetCongesRequests(DemandesSearchDto searchDto);
		public Task UpdateCongeStatus(UpdateCongeStatusDto updateCongeStatusDto);
		public Task DeleteConge(int id);
		public Task AddConge(AddCongeDto congeDto);
		public Task<CongeDto> GetCongeDetails(int id);
		public Task UpdateConge(int id, EditCongeDto congeDto);
    }
}

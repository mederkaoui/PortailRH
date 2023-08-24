using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Dtos.Conge
{
	/// <summary>
	/// CongeDto
	/// </summary>
	public class CongeDto
	{
        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }

		/// <summary>
		/// DateDebut
		/// </summary>
		public DateTime? DateDebut { get; set; }

		/// <summary>
		/// DateFin
		/// </summary>
		public DateTime? DateFin { get; set; }

		/// <summary>
		/// Status
		/// </summary>
		public string? Status { get; set; }

		/// <summary>
		/// EmployeNomComplet
		/// </summary>
		public string? EmployeNomComplet { get; set; }
    }
}

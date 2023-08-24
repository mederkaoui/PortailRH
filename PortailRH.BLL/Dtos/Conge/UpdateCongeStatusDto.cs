using PortailRH.BLL.Dtos.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Dtos.Conge
{
    /// <summary>
    /// UpdateCongeStatuDto
    /// </summary>
    public class UpdateCongeStatusDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// CongeStatusEnum
        /// </summary>
        public CongeStatusEnum Status { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Dtos.Shared
{
    /// <summary>
    /// PaysDto
    /// </summary>
    public class PaysDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Nom
        /// </summary>
        public string? Nom { get; set; }

        /// <summary>
        /// ICollection<VilleDto>
        /// </summary>
        public ICollection<VilleDto>? Villes { get; set; }
    }
}

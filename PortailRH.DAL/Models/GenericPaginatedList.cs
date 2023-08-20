using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.DAL.Models
{
    /// <summary>
    /// GenericPaginatedListDto
    /// </summary>
    public class GenericPaginatedList<TEntity> where TEntity : class
    {
        /// <summary>
        /// IEnumerable<TEntity>
        /// </summary>
        public IEnumerable<TEntity> Entities { get; set; } = new List<TEntity>();

        /// <summary>
        /// CurrentPage
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// ItemsPerPage
        /// </summary>
        public int ItemsPerPage { get; set; }

        /// <summary>
        /// TotalItems
        /// </summary>
        public int TotalItems { get; set; }

        /// <summary>
        /// TotalPages
        /// </summary>
        public int TotalPages { get; set; }
    }
}

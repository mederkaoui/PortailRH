using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.DAL.Repositories.UnitOfWork
{
    /// <summary>
    /// IUnitOfWork
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Commit changes to database
        /// </summary>
        /// <returns>Task<int></returns>
        public Task<int> CommitAsync();
    }
}

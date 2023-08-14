using PortailRH.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.DAL.Repositories.UnitOfWork
{
    /// <summary>
    /// UnitOfWork
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// PortailrhContext
        /// </summary>
        private readonly PortailrhContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">PortailrhContext</param>
        public UnitOfWork(PortailrhContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Commit changes to database
        /// </summary>
        /// <returns>Task<int></returns>
        public Task<int> CommitAsync() => _context.SaveChangesAsync();
    }
}

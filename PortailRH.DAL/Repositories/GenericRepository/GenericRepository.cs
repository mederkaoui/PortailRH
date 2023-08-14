using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;
using PortailRH.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.DAL.Repositories.GenericRepository
{
    /// <summary>
    /// GenericRepository<TEntity>
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// PortailrhContext
        /// </summary>
        private readonly PortailrhContext _context;

        /// <summary>
        /// ILogger<GenericRepository<TEntity>>
        /// </summary>
        private readonly ILogger<GenericRepository<TEntity>> _logger;

        /// <summary>
        /// DbSet<TEntity>
        /// </summary>
        private readonly DbSet<TEntity> _dbSet;

        /// <summary>
        /// Type
        /// </summary>
        private readonly Type EntityType = typeof(TEntity);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">PortailrhContext</param>
        /// <param name="logger">ILogger<GenericRepository<TEntity>></param>
        public GenericRepository(PortailrhContext context, ILogger<GenericRepository<TEntity>> logger)
        {
            _context = context;
            _logger = logger;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task AddedAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);

            _logger.LogInformation($"Adding new entity {EntityType.Name}");
        }

        public async Task AddedAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);

            _logger.LogInformation($"Adding {entities.Count()} entity of {EntityType.Name}");
        }

        public void Delete(object id)
        {
            var entityToDelete = _dbSet.Find(id);

            if (entityToDelete != null)
            {
                _dbSet.Remove(entityToDelete);
            }

            _logger.LogInformation($"Deleting entity {EntityType.Name} with id: {id}");
        }

        public void Delete(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            _dbSet.Remove(entity);

            _logger.LogInformation($"Deleting entity {EntityType.Name} : {@entity}");
        }

        public void Delete(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);

            _logger.LogInformation($"Deleting multiple entites of type {EntityType.Name}");
        }

        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null, bool disableTracking = true)
        {
            _logger.LogInformation($"Getting entity {EntityType.Name} with filter {predicate?.ToString()}");

            IQueryable<TEntity> query = _dbSet;

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                return await orderBy(query).FirstOrDefaultAsync();
            }
            else
            {
                return await query.FirstOrDefaultAsync();
            }
        }

        public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null, bool disableTracking = true)
        {
            _logger.LogInformation($"Getting multiple entities of {EntityType.Name} with the filter {predicate?.ToString()}");

            IQueryable<TEntity> query = _dbSet;

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public void Update(IEnumerable<TEntity> entities)
        {
            _dbSet.UpdateRange(entities);

            _logger.LogInformation($"Updating multiple entities of type {EntityType.Name} with data : {entities}");
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);

            _logger.LogInformation($"Updating entity {EntityType.Name} with data : {entity}");
        }
    }
}

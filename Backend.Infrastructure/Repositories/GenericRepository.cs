using Backend.ApplicationCore.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IDisposable, IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly IApplicationDatabaseContext _context;
        protected readonly DbSet<TEntity> _dbSet;
        public GenericRepository(IApplicationDatabaseContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        /// <summary>
        /// FindAsync find entity by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> FindAsync(object id)
        {
            _dbSet.AsNoTracking();
            return await _dbSet.FindAsync(id);
        }

        /// <summary>
        /// FindAllAsync find all entries Without pagination
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TEntity>> FindAllAsync()
        {
            using var findTask = Task.FromResult(_dbSet.AsEnumerable());
            return await findTask;
        }

        /// <summary>
        /// CountAsync - Count entries
        /// </summary>
        /// <returns></returns>
        public virtual async Task<int> CountAsync()
        {
            using var countTask = _dbSet.CountAsync();
            return await countTask;
        }

        /// <summary>
        /// AddAsync - use to add an Entity without relationship
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        /// <summary>
        /// UpdateAsync update an Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            using var updateTask = Task.FromResult(_dbSet.Update(entity));
            await updateTask;
            return entity;
        }

        public virtual EntityEntry<TEntity> Entry(TEntity entity, TEntity ExistEntity)
        {
           _context.Entry(ExistEntity).State = EntityState.Detached;
           _context.Entry(ExistEntity).CurrentValues.SetValues(entity);
            return _context.Entry(entity);
        }
     

        /// <summary>
        /// DeleteAsync delete en antity by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task DeleteAsync(Guid id)
        {
            var entity = await FindAsync(id);
            await DeleteAsync(entity);
        }
        public virtual async Task DeleteAsync(int id)
        {
            var entity = await FindAsync(id);
            await DeleteAsync(entity);
        }

        /// <summary>
        /// DeleteAsync delete entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task DeleteAsync(TEntity entity)
        {
            using var removeTask = Task.FromResult(_dbSet.Remove(entity));
            await removeTask;
        }

        /// <summary>
        /// SaveChangesAsync save dbContext changes
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            using var saveChangeTask = _context.SaveChangesAsync(cancellationToken);
            return await saveChangeTask;
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            _context?.Dispose();
        }

        
    }
}

using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Backend.ApplicationCore.Interfaces.IRepositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity> FindAsync(object id);

        Task<IEnumerable<TEntity>> FindAllAsync();

        Task<int> CountAsync();

        Task<TEntity> AddAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        EntityEntry<TEntity> Entry(TEntity entity, TEntity ExistEntity); 

        Task DeleteAsync(Guid id);

        Task DeleteAsync(TEntity entity);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        void Dispose();
    }
}
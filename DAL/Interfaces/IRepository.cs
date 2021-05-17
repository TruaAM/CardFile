using Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        IEnumerable<TEntity> GetAll();
        Task<TEntity> GetAsync(Guid id);
        Task<TEntity> FindAsync(Guid id);
        Task CreateAsync(TEntity item);
        void Update(TEntity item);
        void Delete(TEntity entity);
        Task DeleteByIdAsync(Guid id);
    }
}

using Ecommerce.Domain.Contracts.specifications;
using Ecommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Contracts.Repos
{
    public interface IGenaricRepo<TEntity,TKey> where TEntity :BaseEntity<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(TKey id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);

        Task<IEnumerable<TEntity>> GetAllWithSpecificationsAsync(ISpecifications<TEntity,TKey> specifications);
        Task<TEntity?> GetByIdWithSpecificationsAsync(ISpecifications<TEntity, TKey> specifications);
        Task<int> GetCountWithSpecificationsAsync(ISpecifications<TEntity, TKey> specifications);

    }
}

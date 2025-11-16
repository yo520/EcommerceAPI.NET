using Ecommerce.Domain.Contracts.Repos;
using Ecommerce.Domain.Contracts.specifications;
using Ecommerce.Domain.Models;
using Ecommerce.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Persistance.Repos
{
    public class GenaricRepo<TEntity,TKey> : IGenaricRepo<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
    {
        private readonly StoreDbContext context;

        public  GenaricRepo(StoreDbContext Context)
        {
            this.context = Context;
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(TKey id)
        {
           return await context.Set<TEntity>().FindAsync(id);
        }
        public void Add(TEntity entity) => context.Set<TEntity>().Add(entity);

        public void Update(TEntity entity)=> context.Set<TEntity>().Update(entity);
        public void Delete(TEntity entity)=> context.Set<TEntity>().Remove(entity);

        public async Task<IEnumerable<TEntity>> GetAllWithSpecificationsAsync(ISpecifications<TEntity, TKey> specifications)
        {
           var Query=await SpeceificationEvaluator.CreateQuery(context.Set<TEntity>(), specifications).ToListAsync();
            return Query;
        }   

        public async Task<TEntity?> GetByIdWithSpecificationsAsync(ISpecifications<TEntity, TKey> specifications)
        {
            var Query = await SpeceificationEvaluator.CreateQuery(context.Set<TEntity>(), specifications).FirstOrDefaultAsync();
            return Query;
        }

        public async Task<int> GetCountWithSpecificationsAsync(ISpecifications<TEntity, TKey> specifications)
        {
            var Query = await SpeceificationEvaluator.CreateQuery(context.Set<TEntity>(), specifications).CountAsync();
            return Query;
        }
    }
}

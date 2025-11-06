using Ecommerce.Domain.Contracts.Repos;
using Ecommerce.Domain.Contracts.UOW;
using Ecommerce.Domain.Models;
using Ecommerce.Persistance.Contexts;
using Ecommerce.Persistance.Repos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce.Persistance.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _context;
        private readonly Dictionary<string, object> _repositories = new();

        public UnitOfWork(StoreDbContext context)
        {
            _context = context;
        }

        public IGenaricRepo<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var typeName = typeof(TEntity).Name;
            if (_repositories.ContainsKey(typeName))
            {
                return (IGenaricRepo<TEntity, TKey>)_repositories[typeName]!;
            }
            else
            {
                var repository = new GenaricRepo<TEntity, TKey>(_context);
                _repositories.Add(typeName, repository!);
                return repository;
            }
        }   

        public async Task<int> SaveChaingesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}

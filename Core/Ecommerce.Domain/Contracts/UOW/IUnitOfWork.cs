using Ecommerce.Domain.Contracts.Repos;
using Ecommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Contracts.UOW
{
    public interface IUnitOfWork
    {
        IGenaricRepo<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity:BaseEntity<TKey>;
        Task<int> SaveChaingesAsync();

    }
}

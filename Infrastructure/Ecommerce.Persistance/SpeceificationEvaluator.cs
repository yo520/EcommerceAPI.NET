using Ecommerce.Domain.Contracts.specifications;
using Ecommerce.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Persistance
{
    public static class SpeceificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity, TKey>(IQueryable<TEntity> baseQuery, ISpecifications<TEntity, TKey> specifications) where TEntity : BaseEntity<TKey>
        {
            var query=baseQuery;
            if(specifications.Criteria is not null)
            {
                query=query.Where(specifications.Criteria);
            }

            if(specifications.OrderBy is not null)
            {
                query=query.OrderBy(specifications.OrderBy);
            }
            if(specifications.OrderByDescending is not null)
            {
                query=query.OrderByDescending(specifications.OrderByDescending);
            }
            if(specifications.IsPagingEnabled)
            {
                query=query.Skip(specifications.Skip).Take(specifications.Take);
            }

            if (specifications.Includes is not null)
            {
               query= specifications.Includes.Aggregate(query,(current,Exception)=>current.Include(Exception));
            }
            return query;
        }
    }
}

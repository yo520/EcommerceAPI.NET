using Ecommerce.Domain.Contracts.specifications;
using Ecommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Service.Specifications
{
    public abstract class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {

        #region Where
        protected BaseSpecifications(Expression<Func<TEntity, bool>> _Criteria)
        {
            Criteria = _Criteria;
        }
        public Expression<Func<TEntity, bool>> Criteria { get; private set; }

        #endregion


        #region Ordering
        public Expression<Func<TEntity, object>>? OrderBy { get; private set; }
        protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        public Expression<Func<TEntity, object>>? OrderByDescending { get; private set; }
        protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescExpression)
        {
            OrderByDescending = orderByDescExpression;
        }
        #endregion


        #region Includes
        public List<Expression<Func<TEntity, object>>> Includes { get; } = [];
        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        #endregion


        #region Pagination
        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPagingEnabled { get; set; }

        protected void ApplyPaging(int PageIndex, int PageSize)
        {
            IsPagingEnabled = true;
            Take = PageSize;
            Skip = (PageIndex - 1) * PageSize;

        }
        #endregion


    }
}

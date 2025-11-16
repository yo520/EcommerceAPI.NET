using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.Common
{
    public class PaginationResult<TEntity>
    {
        public PaginationResult(int pageNumber, int pageSize, int totalCount, IEnumerable<TEntity> data)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            this.totalCount = totalCount;
            Data = data;
        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int totalCount { get; set; } 
        public IEnumerable<TEntity> Data { get; set; } 
    }
}

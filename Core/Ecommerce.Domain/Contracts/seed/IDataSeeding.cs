using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Contracts.seed
{
    public interface IDataSeeding
    {
        void SeedData();
        Task IdentitySeedData();

    }
}

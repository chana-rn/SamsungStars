using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
   public interface IProductRepository
    {
        Task<List<Product>> getProducts(string? desc, int? minPrice, int? maxPrice, int?[] categoryIds);
    }
}

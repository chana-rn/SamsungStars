using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
   public class ProductRepository:IProductRepository
    {
        SamsungStarsContext _samsungStarsContext;

        public ProductRepository(SamsungStarsContext samsungStarsContext)
        {
            _samsungStarsContext = samsungStarsContext;
        }
        public async Task<List<Product>> getProducts(string? description, int? minPrice, int? maxPrice, int?[] categoryIds)
        {
            var query = _samsungStarsContext.Products.Include(product => product.Category)
            .Where(product =>
            (description == null ? (true) : (product.Description.Contains(description)))
            && (minPrice == null ? (true) : (product.Price >= minPrice))
            && (maxPrice == null ? (true) : (product.Price <= maxPrice))
            && ((categoryIds.Length == 0) ? (true) : (categoryIds.Contains(product.CategoryId)))).OrderBy(product => product.Price);
            return await query.ToListAsync<Product>();
        }

        public async Task<object?> getProducts()//????
        {
            throw new NotImplementedException();
        }
    }
}

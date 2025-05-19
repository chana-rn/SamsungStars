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
        public async Task<List<Product>> getProducts()
        {
            return await _samsungStarsContext.Products.ToListAsync<Product>();
        }

   
    }
}

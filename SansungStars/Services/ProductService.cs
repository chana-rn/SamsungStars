using Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
   public class ProductService:IProductService
    {
        IProductRepository _iProductRepository;
        
      
        public ProductService(IProductRepository iProductRepository)
        {
            _iProductRepository = iProductRepository;
        }
        public async Task<List<Product>> getProducts()
        {
            return await _iProductRepository.getProducts();
        }
    }
}

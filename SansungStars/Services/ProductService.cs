using AutoMapper;
using DTO;
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
        private readonly IMapper _iMapper;
        IProductRepository _iProductRepository;
      
        public ProductService(IProductRepository iProductRepository, IMapper iMapper)
        {
            _iMapper = iMapper;
            _iProductRepository = iProductRepository;
        }
        public async Task<List<ProductDTO>> getProducts(string? desc, int? minPrice, int? maxPrice, int?[] categoryIds)
        {
            var products = await _iProductRepository.getProducts(desc,minPrice, maxPrice,categoryIds); ;
            return _iMapper.Map < List<ProductDTO>>(products);
        }
    }
}

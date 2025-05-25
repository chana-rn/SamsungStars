using Entities;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IProductService
    {
        Task<List<ProductDTO>> getProducts(string? desc, int? minPrice, int? maxPrice, int?[] categoryIds);
    }
}

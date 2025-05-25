using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        SamsungStarsContext _samsungStarsContext;

        public CategoryRepository(SamsungStarsContext samsungStarsContext)
        {
            _samsungStarsContext=samsungStarsContext;
        }
        public async Task<List<Category>> getCategory()
        {
            return await _samsungStarsContext.Categories.Include(category => category.Products).ToListAsync<Category>();
        }
    }
}

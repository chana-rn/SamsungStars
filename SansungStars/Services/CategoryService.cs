using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories;

namespace Services
{
    public class CategoryService : ICategoryService
    {
        ICategoryRepository _iCategoryRepository;
        public CategoryService(ICategoryRepository iCategoryRepository)
        {
            _iCategoryRepository = iCategoryRepository;
        }
        public async Task<List<Category>> getCategory()
        {
            return await _iCategoryRepository.getCategory();
        }
    }
}

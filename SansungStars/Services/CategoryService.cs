﻿using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories;
using AutoMapper;
using DTO;

namespace Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _iMapper;
        ICategoryRepository _iCategoryRepository;
        public CategoryService(ICategoryRepository iCategoryRepository, IMapper iMapper)
        {
            _iMapper = iMapper;
            _iCategoryRepository = iCategoryRepository;
        }
        public async Task<List<CategoryDTO>> getCategory()
        {
            var categories = await _iCategoryRepository.getCategory();
            return categories.Select(m => _iMapper.Map<CategoryDTO>(m)).ToList();
        }
    }
}

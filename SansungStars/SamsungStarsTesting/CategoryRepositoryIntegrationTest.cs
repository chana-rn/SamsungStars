using Entities;
using Repositories;
using Xunit;

namespace TestProject
{
    public class CategoryRepositoryIntegrationTest : IClassFixture<DatabaseFixture>
    {
        private readonly SamsungStarsContext _context;
        private readonly CategoryRepository _repository;

        public CategoryRepositoryIntegrationTest(DatabaseFixture fixture)
        {
            _context = fixture._samsungStars;
            _repository = new CategoryRepository(_context);
        }

        [Fact]
        public async Task GetCategory_ReturnsCategoriesWithProducts()
        {
            // Arrange
            var category = new Category { CategoryName = "Electronics" };
            var product = new Product { ProductName = "TV", Price = 2000, Category = category };
            _context.Categories.Add(category);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // Act
            var categories = await _repository.getCategory();

            // Assert
            Assert.Contains(categories, c => c.CategoryName == "Electronics" && c.Products.Any(p => p.ProductName == "TV"));
        }
    }
}

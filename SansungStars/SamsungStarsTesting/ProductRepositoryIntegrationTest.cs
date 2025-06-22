using Entities;
using Repositories;
using Xunit;

namespace TestProject
{
    public class ProductRepositoryIntegrationTest : IClassFixture<DatabaseFixture>
    {
        private readonly SamsungStarsContext _context;
        private readonly ProductRepository _repository;

        public ProductRepositoryIntegrationTest(DatabaseFixture fixture)
        {
            _context = fixture._samsungStars;
            _repository = new ProductRepository(_context);
        }

        [Fact]
        public async Task GetProducts_FilterByDescriptionAndPriceAndCategory_Works()
        {
            // Arrange
            var category = new Category { CategoryName = "Phones" };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            var product = new Product { ProductName = "iPhone", Price = 3000, Description = "Apple phone", CategoryId = category.CategoryId };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // Act
            var products = await _repository.getProducts("Apple", 2000, 4000, new int?[] { category.CategoryId });

            // Assert
            Assert.Single(products);
            Assert.Equal("iPhone", products[0].ProductName);
        }
    }
}

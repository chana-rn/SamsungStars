using Entities;
using Moq;
using Moq.EntityFrameworkCore;
using Repositories;
using Xunit;

namespace SamsungStarsTesting
{
    public class ProductRepositoryTestingUnit
    {
        [Fact]
        public async Task GetProducts_ReturnsAllProducts()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { ProductId = 1, ProductName = "Galaxy S23", Price = 799.99 },
                new Product { ProductId = 2, ProductName = "Galaxy Tab S8", Price = 649.99 }
            };

            var mockContext = new Mock<SamsungStarsContext>();
            mockContext.Setup(c => c.Products).ReturnsDbSet(products);

            var productRepository = new ProductRepository(mockContext.Object);

            // Act
            var result = await productRepository.getProducts();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Contains(result, p => p.ProductName == "Galaxy S23");
            Assert.Contains(result, p => p.ProductName == "Galaxy Tab S8");
        }

        [Fact]
        public async Task GetProducts_ReturnsEmptyList_WhenNoProductsExist()
        {
            // Arrange
            var products = new List<Product>(); // Empty list

            var mockContext = new Mock<SamsungStarsContext>();
            mockContext.Setup(c => c.Products).ReturnsDbSet(products);

            var productRepository = new ProductRepository(mockContext.Object);

            // Act
            var result = await productRepository.getProducts();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetProducts_HandlesProductsWithInvalidData()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { ProductId = 1, ProductName = null, Price = -10.0 }, // Invalid data
                new Product { ProductId = 2, ProductName = "", Price = 0.0 } // Invalid data
            };

            var mockContext = new Mock<SamsungStarsContext>();
            mockContext.Setup(c => c.Products).ReturnsDbSet(products);

            var productRepository = new ProductRepository(mockContext.Object);

            // Act
            var result = await productRepository.getProducts();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Contains(result, p => p.ProductName == null);
            Assert.Contains(result, p => p.Price == -10.0);
        }

        [Fact]
        public async Task GetProducts_ReturnsDuplicateProducts()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { ProductId = 1, ProductName = "Galaxy S23", Price = 799.99 },
                new Product { ProductId = 1, ProductName = "Galaxy S23", Price = 799.99 } // Duplicate
            };

            var mockContext = new Mock<SamsungStarsContext>();
            mockContext.Setup(c => c.Products).ReturnsDbSet(products);

            var productRepository = new ProductRepository(mockContext.Object);

            // Act
            var result = await productRepository.getProducts();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal(2, result.Count(p => p.ProductName == "Galaxy S23"));
        }
    }
}

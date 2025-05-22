using Entities;
using Moq;
using Moq.EntityFrameworkCore;
using Repositories;
using Xunit;

namespace SamsungStarsTesting
{
    public class CategoryRepositoryTestingUnit
    {
        [Fact]
        public async Task GetCategory_ReturnsAllCategories()
        {
            // Arrange
            var categories = new List<Category>
            {
                new Category { CategoryId = 1, CategoryName = "Electronics" },
                new Category { CategoryId = 2, CategoryName = "Home Appliances" }
            };

            var mockContext = new Mock<SamsungStarsContext>();
            mockContext.Setup(c => c.Categories).ReturnsDbSet(categories);

            var categoryRepository = new CategoryRepository(mockContext.Object);

            // Act
            var result = await categoryRepository.getCategory();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Contains(result, c => c.CategoryName == "Electronics");
            Assert.Contains(result, c => c.CategoryName == "Home Appliances");
        }

        [Fact]
        public async Task GetCategory_ReturnsEmptyList_WhenNoCategoriesExist()
        {
            // Arrange
            var categories = new List<Category>(); // Empty list

            var mockContext = new Mock<SamsungStarsContext>();
            mockContext.Setup(c => c.Categories).ReturnsDbSet(categories);

            var categoryRepository = new CategoryRepository(mockContext.Object);

            // Act
            var result = await categoryRepository.getCategory();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetCategory_HandlesCategoriesWithInvalidData()
        {
            // Arrange
            var categories = new List<Category>
            {
                new Category { CategoryId = 1, CategoryName = null }, // Invalid data
                new Category { CategoryId = 2, CategoryName = "" } // Invalid data
            };

            var mockContext = new Mock<SamsungStarsContext>();
            mockContext.Setup(c => c.Categories).ReturnsDbSet(categories);

            var categoryRepository = new CategoryRepository(mockContext.Object);

            // Act
            var result = await categoryRepository.getCategory();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Contains(result, c => c.CategoryName == null);
            Assert.Contains(result, c => c.CategoryName == "");
        }

        [Fact]
        public async Task GetCategory_ReturnsDuplicateCategories()
        {
            // Arrange
            var categories = new List<Category>
            {
                new Category { CategoryId = 1, CategoryName = "Electronics" },
                new Category { CategoryId = 1, CategoryName = "Electronics" } // Duplicate
            };

            var mockContext = new Mock<SamsungStarsContext>();
            mockContext.Setup(c => c.Categories).ReturnsDbSet(categories);

            var categoryRepository = new CategoryRepository(mockContext.Object);

            // Act
            var result = await categoryRepository.getCategory();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal(2, result.Count(c => c.CategoryName == "Electronics"));
        }
    }
}

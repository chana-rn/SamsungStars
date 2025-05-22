using Entities;
using Moq;
using Moq.EntityFrameworkCore;
using Repositories;
using Xunit;

namespace SamsungStarsTesting
{
    public class OrderRepositoryTestingUnit
    {
        [Fact]
        public async Task AddOrder_ValidOrder_ReturnsAddedOrder()
        {
            // Arrange
            var order = new Order
            {
                OrderDate = DateTime.Now,
                OrderSum = 100.50,
                UserId = 1
            };

            var mockContext = new Mock<SamsungStarsContext>();
            mockContext.Setup(c => c.Orders).ReturnsDbSet(new List<Order>());

            var orderRepository = new OrderRepository(mockContext.Object);

            // Act
            var result = await orderRepository.AddOrder(order);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(order, result);
            mockContext.Verify(c => c.Orders.AddAsync(order, default), Times.Once);
            mockContext.Verify(c => c.SaveChangesAsync(default), Times.Once);
        }

        //[Fact]
        //public async Task AddOrder_NullOrder_ThrowsArgumentNullException()
        //{
        //    // Arrange
        //    var mockContext = new Mock<SamsungStarsContext>();
        //    var orderRepository = new OrderRepository(mockContext.Object);

        //    // Act & Assert
        //    await Assert.ThrowsAsync<ArgumentNullException>(() => orderRepository.AddOrder(null));
        //}

        //[Fact]
        //public async Task AddOrder_InvalidOrder_ThrowsArgumentException()
        //{
        //    // Arrange
        //    var order = new Order
        //    {
        //        OrderDate = DateTime.MinValue, // Invalid date
        //        OrderSum = -50.0, // Negative sum
        //        UserId = 0 // Invalid user ID
        //    };

        //    var mockContext = new Mock<SamsungStarsContext>();
        //    mockContext.Setup(c => c.Orders).ReturnsDbSet(new List<Order>());

        //    var orderRepository = new OrderRepository(mockContext.Object);

        //    // Act & Assert
        //    await Assert.ThrowsAsync<ArgumentException>(() => orderRepository.AddOrder(order));
        //}

        [Fact]
        public async Task AddOrder_DuplicateOrder_AddsSuccessfully()
        {
            // Arrange
            var order = new Order
            {
                OrderId = 1,
                OrderDate = DateTime.Now,
                OrderSum = 100.50,
                UserId = 1
            };

            var orders = new List<Order> { order };

            var mockContext = new Mock<SamsungStarsContext>();
            mockContext.Setup(c => c.Orders).ReturnsDbSet(orders);

            var orderRepository = new OrderRepository(mockContext.Object);

            // Act
            var result = await orderRepository.AddOrder(order);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(order, result);
            mockContext.Verify(c => c.Orders.AddAsync(order, default), Times.Once);
            mockContext.Verify(c => c.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task AddOrder_EmptyDatabase_AddsSuccessfully()
        {
            // Arrange
            var order = new Order
            {
                OrderDate = DateTime.Now,
                OrderSum = 200.75,
                UserId = 2
            };

            var mockContext = new Mock<SamsungStarsContext>();
            mockContext.Setup(c => c.Orders).ReturnsDbSet(new List<Order>());

            var orderRepository = new OrderRepository(mockContext.Object);

            // Act
            var result = await orderRepository.AddOrder(order);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(order, result);
            mockContext.Verify(c => c.Orders.AddAsync(order, default), Times.Once);
            mockContext.Verify(c => c.SaveChangesAsync(default), Times.Once);
        }
    }
}

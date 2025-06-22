using Entities;
using Repositories;
using Xunit;

namespace TestProject
{
    public class OrderRepositoryIntegrationTest : IClassFixture<DatabaseFixture>
    {
        private readonly SamsungStarsContext _context;
        private readonly OrderRepository _repository;

        public OrderRepositoryIntegrationTest(DatabaseFixture fixture)
        {
            _context = fixture._samsungStars;
            _repository = new OrderRepository(_context);
        }

        [Fact]
        public async Task AddOrder_AddsOrderToDatabase()
        {
            // Arrange
            var user = new User { Email = "order@test.com", FirstName = "Order", LastName = "Test", Password = "1234" };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var order = new Order { UserId = user.UserId, OrderDate = DateTime.Now, OrderSum = 500 };

            // Act
            var result = await _repository.AddOrder(order);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.UserId, result.UserId);
            Assert.Contains(_context.Orders, o => o.UserId == user.UserId && o.OrderSum == 500);
        }
    }
}

using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using Services;
using Entities;
using Repositories;
using System.Threading.Tasks;

public class UserServiceTests
{
    [Fact]
    public async Task Login_LogsInformation_WhenLoginSuccessful()
    {
        // Arrange
        var userRepoMock = new Mock<IUserRepository>();
        var loggerMock = new Mock<ILogger<UserService>>();
        var loginRequest = new LoginRequest { Email = "test@email.com", Password = "123456" };
        var user = new User { Email = "test@email.com", Password = "123456" };

        userRepoMock.Setup(r => r.login(loginRequest)).ReturnsAsync(user);

        var service = new UserService(userRepoMock.Object, loggerMock.Object);

        // Act
        await service.login(loginRequest);

        // Assert
        loggerMock.Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("test@email.com")),
                null,
                It.IsAny<Func<It.IsAnyType, Exception, string>>()),
            Times.Once);
    }
}

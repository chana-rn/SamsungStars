using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using Services;

public class UserServicePasswordTests
{
    [Theory]
    [InlineData("123", 0)] // ���� ���
    [InlineData("123456", 1)] // ����
    [InlineData("12345678", 2)] // �������/���� (���� ������ zxcvbn)
    public void CheckPassword_ReturnsExpectedScore(string password, int expectedMinScore)
    {
        var repoMock = new Moq.Mock<IUserRepository>();
        var loggerMock = new Moq.Mock<ILogger<UserService>>();
        var service = new UserService(repoMock.Object, loggerMock.Object);

        var score = service.checkPassword(password);

        Assert.True(score >= expectedMinScore);
    }
}

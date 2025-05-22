using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using Moq;
using Moq.EntityFrameworkCore;
using Repositories;

namespace SamsungStarsTesting
{
    public class UserRepositoryTestingUnit
    {
        [Fact]
        public async Task GetUser_ValidCredentials_ReturnUser()
        {
            //Arrange
            var user = new User { Email = "1", FirstName = "E", LastName = "C", Password = "1234EeCc@" };

            var mockContext = new Mock<SamsungStarsContext>();
            var users = new List<User>() { user };
            mockContext.Setup(u => u.Users).ReturnsDbSet(users);

            var userRepository = new UserRepository(mockContext.Object);
            var loginRequest = new LoginRequest { Email = user.Email, Password = user.Password };

            //Act
            var result = await userRepository.login(loginRequest);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(user, result);
        }

        [Fact]
        public async Task GetUser_InvalidCredentials_ReturnsNull()
        {
            // Arrange
            var user = new User { Email = "1", Password = "1234EeCc@" };

            var mockContext = new Mock<SamsungStarsContext>();
            var users = new List<User>() { user };
            mockContext.Setup(u => u.Users).ReturnsDbSet(users);

            var userRepository = new UserRepository(mockContext.Object);
            var loginRequest = new LoginRequest { Email = "wrong@example.com", Password = "WrongPassword" };

            // Act
            var result = await userRepository.login(loginRequest);

            // Assert
            Assert.Null(result);
        }


        [Fact]
        public async Task Register_ValidUserWithFullDetails_ReturnsRegisteredUser()
        {
            // Arrange
            var user = new User
            {
                Email = "newuser@example.com",
                Password = "Password123",
                FirstName = "John",
                LastName = "Doe"
            };

            var mockContext = new Mock<SamsungStarsContext>();
            mockContext.Setup(c => c.Users).ReturnsDbSet(new List<User>());

            var userRepository = new UserRepository(mockContext.Object);

            // Act
            var result = await userRepository.Register(user);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user, result);
        }


        //[Fact]
        //public async Task Register_UserWithMissingDetails_ThrowsException()
        //{
        //    // Arrange
        //    var user = new User
        //    {
        //        Email = null, // Missing email
        //        Password = "Password123",
        //        FirstName = "John",
        //        LastName = "Doe"
        //    };

        //    var mockContext = new Mock<SamsungStarsContext>();
        //    mockContext.Setup(c => c.Users).ReturnsDbSet(new List<User>());

        //    var userRepository = new UserRepository(mockContext.Object);

        //    // Act & Assert
        //    await Assert.ThrowsAsync<DbUpdateException>(() => userRepository.Register(user));
        //}


        

        //[Fact]
        //public async Task Register_DuplicateUser_ThrowsException()
        //{
        //    // Arrange
        //    var existingUser = new User
        //    {
        //        Email = "duplicate@example.com",
        //        Password = "Password123",
        //        FirstName = "John",
        //        LastName = "Doe"
        //    };

        //    var mockContext = new Mock<SamsungStarsContext>();
        //    mockContext.Setup(c => c.Users).ReturnsDbSet(new List<User> { existingUser });

        //    var userRepository = new UserRepository(mockContext.Object);

        //    var newUser = new User
        //    {
        //        Email = "duplicate@example.com", // Duplicate email
        //        Password = "Password123",
        //        FirstName = "Jane",
        //        LastName = "Smith"
        //    };

        //    // Act & Assert
        //    await Assert.ThrowsAsync<DbUpdateException>(() => userRepository.Register(newUser));
        //}

        [Fact]
        public async Task Update_ValidUser_ReturnsUpdatedUser()
        {
            // Arrange
            var userToUpdate = new User
            {
                UserId = 1,
                Email = "updateduser@example.com",
                Password = "UpdatedPassword123",
                FirstName = "UpdatedFirstName",
                LastName = "UpdatedLastName"
            };

            var mockContext = new Mock<SamsungStarsContext>();
            mockContext.Setup(c => c.Users).ReturnsDbSet(new List<User> { userToUpdate });

            var userRepository = new UserRepository(mockContext.Object);

            // Act
            var result = await userRepository.update(userToUpdate.UserId, userToUpdate);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userToUpdate, result);
            mockContext.Verify(c => c.Users.Update(userToUpdate), Times.Once);
            mockContext.Verify(c => c.SaveChangesAsync(default), Times.Once);
        }


        //[Fact]
        //public async Task Update_NonExistingUser_ThrowsException()
        //{
        //    // Arrange
        //    var userToUpdate = new User
        //    {
        //        UserId = 999, // Non-existing ID
        //        Email = "nonexisting@example.com",
        //        Password = "Password123",
        //        FirstName = "NonExisting",
        //        LastName = "User"
        //    };

        //    var mockContext = new Mock<SamsungStarsContext>();
        //    mockContext.Setup(c => c.Users).ReturnsDbSet(new List<User>());

        //    var userRepository = new UserRepository(mockContext.Object);

        //    // Act & Assert
        //    await Assert.ThrowsAsync<InvalidOperationException>(() => userRepository.update(userToUpdate.UserId, userToUpdate));
        //}

        [Fact]
        public async Task FindById_ExistingUserId_ReturnsUser()
        {
            // Arrange
            var user = new User
            {
                UserId = 1,
                Email = "existinguser@example.com",
                Password = "Password123",
                FirstName = "FirstName",
                LastName = "LastName"
            };

            var mockContext = new Mock<SamsungStarsContext>();
            mockContext.Setup(c => c.Users.FindAsync(user.UserId)).ReturnsAsync(user);

            var userRepository = new UserRepository(mockContext.Object);

            // Act
            var result = await userRepository.findById(user.UserId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user, result);
        }


        [Fact]
        public async Task FindById_NonExistingUserId_ReturnsNull()
        {
            // Arrange
            var mockContext = new Mock<SamsungStarsContext>();
            mockContext.Setup(c => c.Users.FindAsync(It.IsAny<int>())).ReturnsAsync((User)null);

            var userRepository = new UserRepository(mockContext.Object);

            // Act
            var result = await userRepository.findById(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task FindById_InvalidId_ReturnsNull()
        {
            // Arrange
            var mockContext = new Mock<SamsungStarsContext>();
            mockContext.Setup(c => c.Users.FindAsync(It.IsAny<int>())).ReturnsAsync((User)null);

            var userRepository = new UserRepository(mockContext.Object);

            // Act
            var result = await userRepository.findById(-1); // Invalid ID

            // Assert
            Assert.Null(result);
        }
    }


}

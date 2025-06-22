using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamsungStarsTesting
{
    public class UserRepositoryIntegrationTest : IClassFixture<DatabaseFixture>
    {
        
            private readonly SamsungStarsContext _samsungStars;
            private readonly UserRepository _userRepository;

            public UserRepositoryIntegrationTest(DatabaseFixture databaseFixture)
            {
                _samsungStars = databaseFixture.Context;
                _userRepository = new UserRepository(_samsungStars);
            }

            [Fact]
            public async Task Login_ReturnsUser_WhenCredentialsAreCorrect()
            {
                var user = new User { Email = "testUser", Password = "1234", FirstName = "Test", LastName = "User" };
                await _samsungStars.Users.AddAsync(user);
                await _samsungStars.SaveChangesAsync();

                var userLogin = new LoginRequest { Email = "testUser", Password = "1234"};
                var result = await _userRepository.login(userLogin);

                Assert.NotNull(result);
                Assert.Equal("testUser", result.Email);
            }

            [Fact]
            public async Task Register_AddsNewUserToDatabase()
            {
                var user = new User { Email = "newUser", Password = "pass", FirstName = "New", LastName = "User" };
                await _userRepository.Register(user);

                var createdUser = await _samsungStars.Users.FirstOrDefaultAsync(u => u.Email == "newUser");
                Assert.NotNull(createdUser);
                Assert.Equal("New", createdUser.FirstName);
            }

            [Fact]
            public async Task Update_UpdatesUserDetails()
            {
                var user = new User { Email = "updateUser", Password = "pass", FirstName = "Old", LastName = "Name" };
                await _samsungStars.Users.AddAsync(user);
                await _samsungStars.SaveChangesAsync();

                user.FirstName = "New";
                var updatedUser = await _userRepository.update(user.UserId, user);

                Assert.Equal("New", updatedUser.FirstName);
                var userFromDb = await _samsungStars.Users.FindAsync(user.UserId);
                Assert.Equal("New", userFromDb.FirstName);
            }

            [Fact]
            public async Task GetById_ReturnsCorrectUser()
            {
                var user = new User { Email = "byIdUser", Password = "pass", FirstName = "By", LastName = "Id" };
                await _samsungStars.Users.AddAsync(user);
                await _samsungStars.SaveChangesAsync();

                var result = await _userRepository.findById(user.UserId);

                Assert.NotNull(result);
                Assert.Equal("byIdUser", result.Email);
            }
        }
    }


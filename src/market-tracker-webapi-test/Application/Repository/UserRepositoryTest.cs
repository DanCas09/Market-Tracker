using FluentAssertions;
using market_tracker_webapi.Application.Domain;
using market_tracker_webapi.Application.Http.Models;
using market_tracker_webapi.Application.Repository.Operations.User;
using market_tracker_webapi.Infrastructure;
using market_tracker_webapi.Infrastructure.PostgreSQLTables;
using Microsoft.EntityFrameworkCore;

namespace market_tracker_webapi_test.Application.Repository
{
    public class UserRepositoryTest
    {
        [Fact]
        public async Task GetUserByIdAsync_ReturnsObjectAsync()
        {
            // Mock DB
            var expectedUser = new UserEntity
            {
                Id = new Guid("33333333-3333-3333-3333-333333333333"),
                Name = "André",
                Username = "Graca",
                Email = "André@gmail.com",
                Password = "123"
            };

            var mockEntities = new List<UserEntity>
            {
                new()
                {
                    Id = new Guid("11111111-1111-1111-1111-111111111111"),
                    Name = "Diogo",
                    Username = "Digo",
                    Email = "Diogo@gmail.com",
                    Password = "123"
                },
                new()
                {
                    Id = new Guid("22222222-2222-2222-2222-222222222222"),
                    Name = "Daniel",
                    Username = "Dani",
                    Email = "Daniel@gmail.com",
                    Password = "123"
                },
                expectedUser
            };

            var context = CreateDatabase(mockEntities);
            var userRepo = new UserRepository(context);

            // Act
            var actualUser = await userRepo.GetUserByIdAsync(expectedUser.Id);

            // Assert
            actualUser.Should().BeEquivalentTo(expectedUser);
        }

        [Fact]
        public async Task GetUserByIdAsync_ReturnsNullAsync()
        {
            // Mock DB
            var mockEntities = new List<UserEntity>();

            var context = CreateDatabase(mockEntities);
            var userRepo = new UserRepository(context);

            // Act
            var actualUser = await userRepo.GetUserByIdAsync(
                new Guid("11111111-1111-1111-1111-111111111111")
            );

            // Assert
            actualUser.Should().BeNull();
        }

        [Fact]
        public async Task GetUserByUsername_ReturnsObjectAsync()
        {
            // Mock DB
            var expectedUser = new User(
                new Guid("33333333-3333-3333-3333-333333333333"),
                "André",
                "Graca",
                "André@gmail.com",
                "123",
                default
            );

            var mockEntities = new List<UserEntity>
            {
                new()
                {
                    Id = new Guid("11111111-1111-1111-1111-111111111111"),
                    Name = "Diogo",
                    Username = "Digo",
                    Email = "Diogo@gmail.com",
                    Password = "123"
                },
                new()
                {
                    Id = new Guid("22222222-2222-2222-2222-222222222222"),
                    Name = "Daniel",
                    Username = "Dani",
                    Email = "Daniel@gmail.com",
                    Password = "123"
                },
                new()
                {
                    Id = expectedUser.Id,
                    Name = expectedUser.Name,
                    Username = expectedUser.Username,
                    Email = expectedUser.Email,
                    Password = expectedUser.Password
                }
            };

            var context = CreateDatabase(mockEntities);
            var userRepo = new UserRepository(context);

            // Act
            var actualUser = await userRepo.GetUserByUsernameAsync(expectedUser.Username);

            // Assert
            actualUser.Should().BeEquivalentTo(expectedUser);
        }

        [Fact]
        public async Task GetUserByUsernameAsync_ReturnsNullAsync()
        {
            // Mock DB
            var mockEntities = new List<UserEntity>();

            var context = CreateDatabase(mockEntities);
            var userRepo = new UserRepository(context);

            // Act
            var actualUser = await userRepo.GetUserByUsernameAsync("some user name");

            // Assert
            actualUser.Should().BeNull();
        }

        [Fact]
        public async Task GetUserByEmailAsync_ReturnsObjectAsync()
        {
            // Mock DB
            var expectedUser = new User(
                new Guid("33333333-3333-3333-3333-333333333333"),
                "André",
                "Graca",
                "André@gmail.com",
                "123",
                default
            );

            var mockEntities = new List<UserEntity>
            {
                new()
                {
                    Id = new Guid("11111111-1111-1111-1111-111111111111"),
                    Name = "Diogo",
                    Username = "Digo",
                    Email = "Diogo@gmail.com",
                    Password = "123"
                },
                new()
                {
                    Id = new Guid("22222222-2222-2222-2222-222222222222"),
                    Name = "Daniel",
                    Username = "Dani",
                    Email = "Daniel@gmail.com",
                    Password = "123"
                },
                new()
                {
                    Id = expectedUser.Id,
                    Name = expectedUser.Name,
                    Username = expectedUser.Username,
                    Email = expectedUser.Email,
                    Password = expectedUser.Password
                }
            };

            var context = CreateDatabase(mockEntities);
            var userRepo = new UserRepository(context);

            // Act
            var actualUser = await userRepo.GetUserByEmailAsync(expectedUser.Email);

            // Assert
            actualUser.Should().BeEquivalentTo(expectedUser);
        }

        [Fact]
        public async Task GetUserByEmailAsync_ReturnsNullAsync()
        {
            // Mock DB
            var mockEntities = new List<UserEntity>();

            var context = CreateDatabase(mockEntities);
            var userRepo = new UserRepository(context);

            // Act
            var actualUser = await userRepo.GetUserByEmailAsync("some email");

            // Assert
            actualUser.Should().BeNull();
        }

        [Fact]
        public async Task CreateUserAsync_ReturnsObjectAsync()
        {
            // Mock DB
            var mockEntities = new List<UserEntity>();

            var context = CreateDatabase(mockEntities);
            var userRepo = new UserRepository(context);

            // Act
            var actualId = await userRepo.CreateUserAsync(
                "Diogo",
                "Digo",
                "Diogo@gmail.com",
                "123"
            );

            // Assert
            actualId.Should().NotBeEmpty();
        }

        [Fact]
        public async Task UpdateUserAsync_ReturnsObjectAsync()
        {
            // Mock DB
            var expectedUserId = new Guid("11111111-1111-1111-1111-111111111111");

            var mockEntities = new List<UserEntity>
            {
                new()
                {
                    Id = expectedUserId,
                    Name = "Diogo",
                    Username = "Digo",
                    Email = "Diogo@gmail.com",
                    Password = "123"
                }
            };

            var context = CreateDatabase(mockEntities);
            var userRepo = new UserRepository(context);

            // Act
            var actualUserDetails = await userRepo.UpdateUserAsync(
                expectedUserId,
                "Diogo Santos",
                "DiogoFAS"
            );

            // Assert
            actualUserDetails
                .Should()
                .BeEquivalentTo(await userRepo.GetUserByIdAsync(expectedUserId));
        }

        [Fact]
        public async Task UpdateUserAsync_ReturnsNullAsync()
        {
            // Mock DB
            var mockEntities = new List<UserEntity>();

            var context = CreateDatabase(mockEntities);
            var userRepo = new UserRepository(context);

            // Act
            var actualUserDetails = await userRepo.UpdateUserAsync(
                new Guid("11111111-1111-1111-1111-111111111111"),
                "Diogo Santos",
                "DiogoFAS"
            );

            // Assert
            actualUserDetails.Should().BeNull();
        }

        [Fact]
        public async Task DeleteUserAsync_ReturnsObjectAsync()
        {
            // Mock DB
            var expectedDeletedUser = new UserEntity
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Name = "Diogo",
                Username = "Digo",
                Email = "digo@gmail.com",
                Password = "123"
            };

            var context = CreateDatabase([expectedDeletedUser]);
            var userRepo = new UserRepository(context);

            // Act
            var actualDeletedUserData = await userRepo.DeleteUserAsync(expectedDeletedUser.Id);

            // Assert
            actualDeletedUserData.Should().NotBeNull();
            actualDeletedUserData.Should().BeEquivalentTo(expectedDeletedUser);
        }

        [Fact]
        public async Task DeleteUserAsync_ReturnsNullAsync()
        {
            // Mock DB
            var mockEntities = new List<UserEntity>();

            var context = CreateDatabase(mockEntities);
            var userRepo = new UserRepository(context);

            // Act
            var actualDeletedUserData = await userRepo.DeleteUserAsync(
                new Guid("11111111-1111-1111-1111-111111111111")
            );

            // Assert
            actualDeletedUserData.Should().BeNull();
        }

        private static MarketTrackerDataContext CreateDatabase(List<UserEntity> userEntities)
        {
            DbContextOptions<MarketTrackerDataContext> options =
                new DbContextOptionsBuilder<MarketTrackerDataContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                    .Options;

            var databaseContext = new MarketTrackerDataContext(options);
            databaseContext.User.AddRange(userEntities);
            databaseContext.SaveChanges();
            return databaseContext;
        }
    }
}

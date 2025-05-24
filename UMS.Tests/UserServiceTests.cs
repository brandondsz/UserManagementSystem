using Moq;
using UMS.Application.DTOs;
using UMS.Application.Services;
using UMS.Core.Entities;
using UMS.Core.Interfaces;

namespace UMS.Tests
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _mockRepo;
        private readonly UserService _userService;
        const string firstName = "Test";
        const string lastName = "User";
        const string email = "test@test.com";
        public UserServiceTests()
        {
            _mockRepo = new Mock<IUserRepository>();
            _userService = new UserService(_mockRepo.Object);
        }
        [Fact]
        public async Task CreateUserAsync_ShouldSaveUser()
        {
            //arrange
            var dto = new CreateUserDto { FirstName = firstName, LastName = lastName, Email = email };
            //act
            await _userService.CreateUserAsync(dto);
            //assert
            _mockRepo.Verify(x => x.CreateAsync(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task GetUserAsync_ShouldReturnUser_WhenUserExists()
        {
            //arrange
            var userId = Guid.NewGuid();

            var user = new User { Id = userId, FirstName = firstName, LastName = lastName, Email = email };
            _mockRepo.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync(user);

            //act
            var result = await _userService.GetUserAsync(userId);

            //assert
            Assert.Equal(userId, result.Id);
            Assert.Equal(firstName, result.FirstName);
            Assert.Equal(lastName, result.LastName);
            Assert.Equal(email, result.Email);
        }

        [Fact]
        public async Task GetUserAsync_ShouldThrow_WhenUserDoesNotExist()
        {
            //arrange
            var userId = Guid.NewGuid();
            _mockRepo.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync((User)null);

            //act & assert
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                _userService.GetUserAsync(userId));

            Assert.Equal($"User {userId} not found.", exception.Message);
        }

        [Fact]
        public async Task UpdateUserAsync_ShouldUpdate()
        {
            //arrange
            var userId = Guid.NewGuid();

            var user = new User { Id = userId, FirstName = firstName, LastName = lastName, Email = email };
            var updateDto = new UserDto
            {
                FirstName = "New Name",
                Email = "new@email.com"
            };
            _mockRepo.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync(user);

            _mockRepo.Setup(r => r.UpdateAsync(It.IsAny<User>()))
            .Returns(Task.CompletedTask);

            //act
            await _userService.UpdateUserAsync(userId, updateDto);

            //assert
            _mockRepo.Verify(r => r.GetByIdAsync(userId), Times.Once);
            _mockRepo.Verify(r => r.UpdateAsync(It.Is<User>(u =>
                u.Id == userId &&
                u.FirstName == updateDto.FirstName &&
                u.LastName == updateDto.LastName &&
                u.Email == updateDto.Email
            )), Times.Once);

        }

        [Fact]
        public void GetAll_ShouldReturnListOfUserDtos()
        {
            //arrange
            var users = new List<User>
            {
                new User { Id = Guid.NewGuid(), FirstName = "Alice", LastName="Test", Email = "alice@email.com" },
                new User { Id = Guid.NewGuid(), FirstName = "Bob", LastName="Test", Email = "bob@email.com" }
            }.AsQueryable();

            _mockRepo.Setup(r => r.GetAllIQueryable()).Returns(users);


            //act
            var result = _userService.GetAll();

            //assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Contains(result, u => u.FirstName == "Alice" && u.LastName == "Test" && u.Email == "alice@email.com");
            Assert.Contains(result, u => u.FirstName == "Bob" && u.LastName == "Test" && u.Email == "bob@email.com");

            _mockRepo.Verify(r => r.GetAllIQueryable(), Times.Once);
        }
    }

}
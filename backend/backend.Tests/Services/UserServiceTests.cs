using backend.DTO;
using backend.Models;
using backend.Repository.Interfaces;
using backend.ServiceLayer;
using Moq;

namespace backend.Tests.Services;

[TestClass]
public class UserServiceTests
{
    private Mock<IUserRepo> _userRepo = null!;
    private UserService _service = null!;

    [TestInitialize]
    public void Setup()
    {
        _userRepo = new Mock<IUserRepo>();
        _service = new UserService(_userRepo.Object);
    }

    [TestMethod]
    public async Task GetAllUsers_WithData()
    {
        _userRepo.Setup(r => r.GetAllUsers()).ReturnsAsync(
        [
            new User { Id = 1, Name = "Ada", Age = 36, City = "London", State = "England", Pincode = "SW1A" }
        ]);

        var result = (await _service.GetAllUsers()).ToList();

        Assert.AreEqual(1, result.Count);
        Assert.AreEqual("Ada", result[0].Name);
        Assert.AreEqual(36, result[0].Age);
    }

    [TestMethod]
    public async Task GetUserById_ReturnsNull()
    {
        _userRepo.Setup(r => r.GetUserById(99)).ReturnsAsync((User?)null);

        var result = await _service.GetUserById(99);

        Assert.IsNull(result);
    }

    [TestMethod]
    public async Task AddUser_ReturnData()
    {
        var dto = new UserDto { Name = "Ada", Age = 36, City = "London", State = "England", Pincode = "SW1A" };
        _userRepo
            .Setup(r => r.AddUser(It.IsAny<User>()))
            .ReturnsAsync((User u) =>
            {
                u.Id = 7;
                return u;
            });

        var result = await _service.AddUser(dto);

        Assert.AreEqual(7, result.Id);
        Assert.AreEqual("Ada", result.Name);
        _userRepo.Verify(r => r.AddUser(It.Is<User>(u => u.Name == "Ada" && u.Age == 36)), Times.Once);
    }

    [TestMethod]
    public async Task UpdateUser_ReturnsFalse()
    {
        _userRepo.Setup(r => r.GetUserById(5)).ReturnsAsync((User?)null);

        var result = await _service.UpdateUser(5, new UserDto { Name = "X", Age = 1, City = "A", State = "B", Pincode = "1" });

        Assert.IsFalse(result);
        _userRepo.Verify(r => r.UpdateUser(It.IsAny<User>()), Times.Never);
    }

    [TestMethod]
    public async Task UpdateUser_ReturnsTrue()
    {
        var existing = new User { Id = 1, Name = "Old", Age = 20, City = "A", State = "B", Pincode = "1" };
        _userRepo.Setup(r => r.GetUserById(1)).ReturnsAsync(existing);
        _userRepo.Setup(r => r.UpdateUser(existing)).Returns(Task.CompletedTask);

        var result = await _service.UpdateUser(1, new UserDto
        {
            Name = "New",
            Age = 30,
            City = "C",
            State = "D",
            Pincode = "2"
        });

        Assert.IsTrue(result);
        Assert.AreEqual("New", existing.Name);
        Assert.AreEqual(30, existing.Age);
        _userRepo.Verify(r => r.UpdateUser(existing), Times.Once);
    }

    [TestMethod]
    public async Task DeleteUser_ReturnsTrue()
    {
        var existing = new User { Id = 1, Name = "Ada", Age = 36, City = "London", State = "England", Pincode = "SW1A" };
        _userRepo.Setup(r => r.GetUserById(1)).ReturnsAsync(existing);
        _userRepo.Setup(r => r.DeleteUser(existing)).Returns(Task.CompletedTask);

        var result = await _service.DeleteUser(1);

        Assert.IsTrue(result);
        _userRepo.Verify(r => r.DeleteUser(existing), Times.Once);
    }
}

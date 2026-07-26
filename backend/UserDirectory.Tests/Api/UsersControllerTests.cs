using Microsoft.AspNetCore.Mvc;
using Moq;
using UserDirectory.Api.Controllers;
using UserDirectory.Application.DTOs;
using UserDirectory.Application.Users;

namespace UserDirectory.Tests.Api;

[TestClass]
public class UsersControllerTests
{
    private Mock<IUserService> _userService = null!;
    private UsersController _controller = null!;

    [TestInitialize]
    public void Setup()
    {
        _userService = new Mock<IUserService>();
        _controller = new UsersController(_userService.Object);
    }

    [TestMethod]
    public async Task GetAllUsers_ReturnsOk()
    {
        var users = new List<UserDto>
        {
            new() { Id = 1, Name = "Ada", Age = 36, City = "London", State = "England", Pincode = "SW1A" }
        };
        _userService.Setup(s => s.GetAllUsers()).ReturnsAsync(users);

        var result = await _controller.GetAllUsers();

        var ok = result as OkObjectResult;
        Assert.IsNotNull(ok);
        Assert.AreEqual(200, ok.StatusCode);
        Assert.AreEqual(users, ok.Value);
    }

    [TestMethod]
    public async Task GetUserById_ReturnsOk()
    {
        var user = new UserDto { Id = 1, Name = "Ada", Age = 36, City = "London", State = "England", Pincode = "SW1A" };
        _userService.Setup(s => s.GetUserById(1)).ReturnsAsync(user);

        var result = await _controller.GetUserById(1);

        var ok = result as OkObjectResult;
        Assert.IsNotNull(ok);
        Assert.AreEqual(user, ok.Value);
    }

    [TestMethod]
    public async Task GetUserById_ReturnsNotFound()
    {
        _userService.Setup(s => s.GetUserById(99)).ReturnsAsync((UserDto?)null);

        var result = await _controller.GetUserById(99);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }

    [TestMethod]
    public async Task AddUser_ReturnsCreatedAtAction()
    {
        var dto = new UserDto { Name = "Ada", Age = 36, City = "London", State = "England", Pincode = "SW1A" };
        var created = new UserDto { Id = 10, Name = "Ada", Age = 36, City = "London", State = "England", Pincode = "SW1A" };
        _userService.Setup(s => s.AddUser(dto)).ReturnsAsync(created);

        var result = await _controller.AddUser(dto);

        var createdResult = result as CreatedAtActionResult;
        Assert.IsNotNull(createdResult);
        Assert.AreEqual(nameof(UsersController.GetUserById), createdResult.ActionName);
        Assert.AreEqual(created, createdResult.Value);
    }

    [TestMethod]
    public async Task UpdateUser_ReturnsNotFound()
    {
        var dto = new UserDto { Name = "Ada", Age = 36, City = "London", State = "England", Pincode = "SW1A" };
        _userService.Setup(s => s.UpdateUser(5, dto)).ReturnsAsync(false);

        var result = await _controller.UpdateUser(5, dto);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }

    [TestMethod]
    public async Task UpdateUser_ReturnsNoContent()
    {
        var dto = new UserDto { Name = "Ada", Age = 36, City = "London", State = "England", Pincode = "SW1A" };
        _userService.Setup(s => s.UpdateUser(1, dto)).ReturnsAsync(true);

        var result = await _controller.UpdateUser(1, dto);

        Assert.IsInstanceOfType(result, typeof(NoContentResult));
    }

    [TestMethod]
    public async Task DeleteUser_ReturnsNotFound()
    {
        _userService.Setup(s => s.DeleteUser(5)).ReturnsAsync(false);

        var result = await _controller.DeleteUser(5);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }

    [TestMethod]
    public async Task DeleteUser_ReturnsNoContent()
    {
        _userService.Setup(s => s.DeleteUser(1)).ReturnsAsync(true);

        var result = await _controller.DeleteUser(1);

        Assert.IsInstanceOfType(result, typeof(NoContentResult));
    }
}

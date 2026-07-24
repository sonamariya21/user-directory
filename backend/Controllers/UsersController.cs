using backend.DTO;
using backend.ServiceLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Authorize]
[ApiController]
[Route("api/user-directory")]
public class UsersController : ControllerBase
{
    private readonly IUserService _service;

    public UsersController(IUserService service)
    {
        _service = service;
    }

    #region PUBLIC METHODS
    [HttpGet("get-all-users")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _service.GetAllUsers();

        return Ok(users);
    }

    [HttpGet("get-user-by-id/{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await _service.GetUserById(id);

        if (user == null)
            return NotFound();

        return Ok(user);
    }

    [HttpPost("add-user")]
    public async Task<IActionResult> AddUser(UserDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await _service.AddUser(dto);

        return CreatedAtAction(nameof(GetUserById),
            new { id = user.Id },
            user);
    }

    [HttpPut("update-user/{id}")]
    public async Task<IActionResult> UpdateUser(int id, UserDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updated = await _service.UpdateUser(id, dto);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("delete-user/{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var deleted = await _service.DeleteUser(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }

    #endregion PUBLIC METHODS
}
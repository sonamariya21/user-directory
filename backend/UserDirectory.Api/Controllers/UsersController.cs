using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserDirectory.Application.DTOs;
using UserDirectory.Application.Users;

namespace UserDirectory.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/user-directory")]
public class UsersController : ControllerBase
{
    private readonly IUserService _service;

    #region CONSTRUCTOR
    public UsersController(IUserService service)
    {
        _service = service;
    }
    #endregion

    #region PUBLIC METHODS
    /// <summary>
    /// Get all users list
    /// </summary>
    /// <returns></returns>
    [HttpGet("get-all-users")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _service.GetAllUsers();
        return Ok(users);
    }
    /// <summary>
    /// Get user details by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("get-user-by-id/{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await _service.GetUserById(id);

        if (user == null)
            return NotFound();

        return Ok(user);
    }
    /// <summary>
    /// Add user details into the table
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
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
    /// <summary>
    /// Update user details by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
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
    /// <summary>
    /// Delete user details from table by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("delete-user/{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var deleted = await _service.DeleteUser(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
    #endregion
}

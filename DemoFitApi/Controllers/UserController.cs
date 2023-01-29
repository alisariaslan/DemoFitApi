using DemoFitApi.Models;
using DemoFitApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;

[ApiController]
[Route("[controller]")]
public class UserController : Controller
{
	private readonly IUserService _userservice;

	public UserController(IUserService userService)
	{
		_userservice = userService;
	}

	[Authorize]
	[HttpGet]
	public async Task<IActionResult> Get()
	{
		var result = await _userservice.GetUserList();
		if (result != null)
			return Ok(result);
		return NotFound();
	}

	[Authorize]
	[HttpGet("{id:int}")]
	public async Task<IActionResult> GetUser(int id)
	{
		var result = await _userservice.GetUser_wId(id);
		if (result != null)
			return Ok(result);
		return NotFound();
	}

	[Authorize]
	[HttpGet("{username}")]
	public async Task<IActionResult> GetUser_wName(string username)
	{
		var result = await _userservice.GetUser_wName(username);
		if (result != null)
			return Ok(result);
		return NotFound();
		//return Ok(username);
	}

	[Authorize]
	[HttpPost]
	public async Task<IActionResult> AddUser([FromBody] User employee)
	{
		var result = await _userservice.CreateUser(employee);

		return Ok(result);
	}

	[Authorize]
	[HttpPut]
	public async Task<IActionResult> UpdateEmployee([FromBody] User employee)
	{
		var result = await _userservice.UpdateUser(employee);

		return Ok(result);
	}

	[Authorize]
	[HttpDelete("{id:int}")]
	public async Task<IActionResult> DeleteEmployee(int id)
	{
		var result = await _userservice.DeleteUser(id);

		return Ok(result);
	}
}
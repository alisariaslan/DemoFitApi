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
	[HttpGet("all")]
	public async Task<IActionResult> Get()
	{
		var result = await _userservice.GetUserList();
		if (result != null)
			return Ok(result);
		return NotFound();
	}

	[Authorize]
	[HttpGet("id=/{id:int}")]
	public async Task<IActionResult> GetUser_wId(int id)
	{
		var result = await _userservice.GetUser_wId(id);
		if (result != null)
			return Ok(result);
		return NotFound();
	}

	[Authorize]
	[HttpDelete("id=/{id:int}")]
	public async Task<IActionResult> DeleteUser_wId(int id)
	{
		var result = await _userservice.DeleteUser_wId(id);
		if (result != false)
			return Ok(result);
		return NotFound();
	}

	[Authorize]
	[HttpGet("name=/{username}")]
	public async Task<IActionResult> GetUser_wName(string username)
	{
		var result = await _userservice.GetUser_wName(username);
		if (result != null)
			return Ok(result);
		return NotFound();
		//return Ok(username);
	}

	[Authorize]
	[HttpDelete("name=/{username}")]
	public async Task<IActionResult> DeleteUser_wName(string username)
	{
		var result = await _userservice.DeleteUser_wName(username);
		if (result != false)
			return Ok(result);
		return NotFound();
	}

	[Authorize]
	[HttpPost("new")]
	public async Task<IActionResult> AddUser([FromBody] User employee)
	{
		bool result = await _userservice.CreateUser(employee);
		if (result)
			return Ok(result);
		return BadRequest();
	}

	[Authorize]
	[HttpPut("save")]
	public async Task<IActionResult> UpdateUser([FromBody] User user)
	{
		bool result = await _userservice.UpdateUser(user);
		if (result)
			return Ok(result);
		return NotFound();
	}

	[Authorize]
	[HttpPut("loggedUserid=/{id:int}")]
	public async Task<IActionResult> UpdateLog(int id)
	{
		bool result = await _userservice.UpdateLog(id);
		if (result)
			return Ok(result);
		return NotFound();
	}




}
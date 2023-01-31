using DemoFitApi.Models;
using DemoFitApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;

[ApiController]
[Route("[controller]")]
public class ProductController : Controller
{
	private readonly IProductService iservice;

	public ProductController(IProductService iservice)
	{
		this.iservice = iservice;
	}

	[Authorize]
	[HttpGet("all")]
	public async Task<IActionResult> Get()
	{
		var result = await iservice.GetProductList();
		if (result != null)
			return Ok(result);
		return NotFound();
	}


}
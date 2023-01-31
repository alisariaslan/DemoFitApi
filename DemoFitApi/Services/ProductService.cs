using DemoFitApi.Models;
using System.Diagnostics;

namespace DemoFitApi.Services
{
	public class ProductService :IProductService
	{
		private readonly IDbService _dbService;

		public ProductService(IDbService dbService)
		{
			_dbService = dbService;
		}

		public async Task<List<Product>> GetProductList()
		{
			var employeeList = await _dbService.GetAll<Product>("SELECT * FROM public.products", new { });
			return employeeList;
		}
	}
}

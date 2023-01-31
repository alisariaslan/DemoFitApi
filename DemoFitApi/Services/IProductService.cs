using DemoFitApi.Models;

namespace DemoFitApi.Services
{
	public interface IProductService
	{
		Task<List<Product>> GetProductList();
	}
}

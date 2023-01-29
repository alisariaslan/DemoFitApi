using DemoFitApi.Models;

namespace DemoFitApi.Services
{
	public interface IUserService
	{
		Task<bool> CreateUser(User employee);
		Task<List<User>> GetUserList();
		Task<User> UpdateUser(User employee);
		Task<bool> DeleteUser(int key);

		Task<User> GetUser_wId(int key);

		Task<User> GetUser_wName(string username);
	}
}

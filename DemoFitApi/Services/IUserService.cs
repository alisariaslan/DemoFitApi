using DemoFitApi.Models;

namespace DemoFitApi.Services
{
	public interface IUserService
	{
		Task<bool> CreateUser(User employee);
		Task<List<User>> GetUserList();
		Task<bool> UpdateUser(User employee);

		Task<bool> UpdateLog(int id);
		Task<bool> DeleteUser_wId(int key);

		Task<bool> DeleteUser_wName(string username);

		Task<User> GetUser_wId(int key);

		Task<User> GetUser_wName(string username);
	}
}

using DemoFitApi.Models;
using System.Diagnostics;

namespace DemoFitApi.Services
{
	public class UserService : IUserService
	{
		private readonly IDbService _dbService;

		public UserService(IDbService dbService)
		{
			_dbService = dbService;
		}

		public async Task<bool> CreateUser(User employee)
		{
			var result =
				await _dbService.EditData(
					"INSERT INTO public.users (id,username, password) VALUES (@Id, @Username, @Password)",
					employee);
			return true;
		}

		public async Task<List<User>> GetUserList()
		{
			var employeeList = await _dbService.GetAll<User>("SELECT * FROM public.users", new { });
			return employeeList;
		}


		public async Task<User> GetUser_wId(int id)
		{
			var employeeList = await _dbService.GetAsync<User>("SELECT * FROM public.users where id=@id", new { id });
			return employeeList;
		}

		public async Task<User> GetUser_wName(string username)
		{
			var employeeList = await _dbService.GetAsync<User>("SELECT * FROM public.users where username=@username", new { username });
			return employeeList;
		}

		public async Task<User> UpdateUser(User employee)
		{
			var updateEmployee =
				await _dbService.EditData(
					"Update public.users SET username=@Username, password=@Password WHERE id=@Id",
					employee);
			return employee;
		}

		public async Task<bool> DeleteUser(int id)
		{
			var deleteEmployee = await _dbService.EditData("DELETE FROM public.users WHERE id=@Id", new { id });
			return true;
		}
	}
}

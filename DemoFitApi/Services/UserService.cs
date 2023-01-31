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

		public async Task<bool> CreateUser(User user)
		{
			var myuser = await _dbService.GetAsync<User>("SELECT * FROM public.users where username=@Username", user );
			if (myuser != null)
				return false;
			 await _dbService.EditData("INSERT INTO public.users (\"username\", \"password\", \"registerdate\") VALUES (@Username, @Password, pg_catalog.now())", user);
			 await _dbService.EditData("INSERT INTO public.memberships (\"type\", \"start\", \"end\") VALUES (1,'2023-02-01','2023-05-01')", user);
			 await _dbService.EditData("INSERT INTO public.logs (\"loggedcount\") VALUES (0)", user);
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

		public async Task<bool> DeleteUser_wId(int id)
		{
			int[] deleted = new int[3];
			deleted[0] = await _dbService.EditData("DELETE FROM public.logs WHERE id=@id", new { id });
			deleted[1] = await _dbService.EditData("DELETE FROM public.memberships WHERE id=@id", new { id });
			deleted[2] = await _dbService.EditData("DELETE FROM public.users WHERE id=@id", new { id });
			if (deleted[0] == 1 && deleted[1] == 1 && deleted[2] == 1)
				return true;
			else
				return false;
		}

		public async Task<User> GetUser_wName(string username)
		{
			var employeeList = await _dbService.GetAsync<User>("SELECT * FROM public.users where username=@Username", new { username });
			return employeeList;
		}

		public async Task<bool> DeleteUser_wName(string username)
		{
			var user = await _dbService.GetAsync<User>("SELECT * FROM public.users where username=@Username", new { username });
			if(user==null)
				return false;
			int id = user.Id;
			int[] deleted = new int[3];
			deleted[0] = await _dbService.EditData("DELETE FROM public.logs WHERE id=@id", new { id });
			deleted[1] = await _dbService.EditData("DELETE FROM public.memberships WHERE id=@id", new { id });
			deleted[2] = await _dbService.EditData("DELETE FROM public.users WHERE id=@id", new { id });
			if (deleted[0] == 1 && deleted[1] == 1 && deleted[2] == 1)
				return true;
			else
				return false;
		}

		public async Task<User> UpdateUser(User employee)
		{
			var updateEmployee =
				await _dbService.EditData(
					"Update public.users SET username=@Username, password=@Password WHERE id=@Id",
					employee);
			return employee;
		}

		public async Task<bool> UpdateLog(int id)
		{
			User newuser = new User();
			newuser.Id = id; 
			int updatedOne = await _dbService.EditData($"UPDATE logs SET loggedcount = ((select le.loggedcount FROM logs le WHERE le.id =@Id)+1) WHERE id =@Id", newuser);
			int updatedTwo = await _dbService.EditData($"UPDATE logs SET lastloggedin = pg_catalog.now() WHERE id =@Id", newuser);
			if (updatedOne== 1 && updatedTwo == 1)
				return true;
			return false;
		}

	}
}

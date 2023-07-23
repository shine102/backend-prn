namespace LibraryManagementBackend.Repositories.User
{
	using LibraryManagementBackend.Models;
	public interface IUserRepository : IRepository<User>
	{
		public Task<User> GetByUsernameAsync(string username);
		public Task<User> UpdatePhone(string phone, int id);

	}
}

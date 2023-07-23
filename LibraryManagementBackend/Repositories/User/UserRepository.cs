namespace LibraryManagementBackend.Repositories.User
{
	using LibraryManagementBackend.Models;
	using Microsoft.EntityFrameworkCore;
	using System.Threading.Tasks;

	public class UserRepository : Repository<User>, IUserRepository
	{
		public UserRepository(LibraryManagementDbContext context) : base(context)
		{
		}
		protected override DbSet<User> DbSet => this.context.Users;

		public Task<User> GetByUsernameAsync(string username)
		{
			User user = this.DbSet.FirstOrDefaultAsync(user => user.Username == username).Result;
			return Task.FromResult(user);
		}

		public Task<User> UpdatePhone(string phone, int id)
		{
			User user = this.DbSet.FirstOrDefaultAsync(user => user.Id == id).Result;
			user.Phone = phone;
			this.context.SaveChanges();
			return Task.FromResult(user);
		}
	}
}

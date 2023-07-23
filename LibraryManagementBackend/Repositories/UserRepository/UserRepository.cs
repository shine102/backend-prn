using LibraryManagementBackend.DTO.UserDTO;
using LibraryManagementBackend.Models;
using Microsoft.EntityFrameworkCore;
using System.Security;

namespace LibraryManagementBackend.Repositories.UserRepository
{
    public class UserRepository : Repository<User>
    {
        private LibraryManagementDbContext context { get; set; }
        public UserRepository(LibraryManagementDbContext context) : base(context)
        {
            this.context = context;
        }

       
        public async Task<User> GetUserByName(string userName)
        {
            var allUser = await this.GetAllAsync();
            var user  = allUser.FirstOrDefault(e=>e.Username.Equals(userName));
            return user;
        }

        protected override DbSet<User> DbSet => this.context.Users;
    }
}

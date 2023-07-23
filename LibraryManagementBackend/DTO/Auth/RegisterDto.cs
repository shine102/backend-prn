using LibraryManagementBackend.Models;

namespace LibraryManagementBackend.DTO.Auth
{
	public class RegisterDto : IRequestDto<User>
	{
		public required string Username { get; set; }
		public required string Password { get; set; }
		public required string Phone { get; set; }
		public required string CredentialCode { get; set; }

		public void PopulateEntity(User entity)
		{
			entity.Phone = this.Phone;
			entity.Username = this.Username;
			entity.Password = this.Password;
			entity.CredentialCode = this.CredentialCode;
		}
	}
}

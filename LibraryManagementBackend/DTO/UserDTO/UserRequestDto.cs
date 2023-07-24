using LibraryManagementBackend.Models;

namespace LibraryManagementBackend.DTO.UserDTO
{
    public class UserRequestDto : IRequestDto<User>
    {
        public string Username { get; set; }

        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string CredentialCode { get; set; }
       
        public void PopulateEntity(User entity)
        {
            entity.Username = this.Username;
            entity.Password = this.Password;
            entity.Phone = this.PhoneNumber;
            entity.CredentialCode = this.CredentialCode;
        }
    }
}

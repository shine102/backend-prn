using LibraryManagementBackend.Models;

namespace LibraryManagementBackend.DTO.UserDTO
{
    public class UserRequestDto : IRequestDto<User>
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        public string CredentialCode { get; set; }
        public void Populate(User entity)
        {
            this.Id = entity.Id;
            this.Username = entity.Username;
            this.PhoneNumber = entity.Phone;
            this.CredentialCode = entity.CredentialCode;
        }
    }
}

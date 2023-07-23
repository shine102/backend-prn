using LibraryManagementBackend.Models;

namespace LibraryManagementBackend.DTO.UserDTO
{
    public class UserUpdatePhoneDto : IRequestDto<User>
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public void PopulateEntity(User entity)
        {
            this.Id = entity.Id;
            this.PhoneNumber = entity.Phone;
        }
    }
}

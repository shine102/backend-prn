using LibraryManagementBackend.Models;

namespace LibraryManagementBackend.DTO.UserDTO
{
    public class UserResponseDto : IResponseDto<User, UserResponseDto>
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        public string CredentialCode { get; set; }
        public static UserResponseDto FromEntity(User entity)
        {
            return new UserResponseDto
            {
                Id = entity.Id,
                Username = entity.Username,
                PhoneNumber = entity.Phone,
                CredentialCode = entity.CredentialCode
            };
        }
    }
}

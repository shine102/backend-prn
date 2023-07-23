using LibraryManagementBackend.DTO.UserDTO;
using LibraryManagementBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LibraryManagementBackend.Repositories.User;

namespace LibraryManagementBackend.Controllers
{
    [ApiController]
    [Route("Api/[Controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserRepository userRepository;
        public UserController(UserRepository userRepository) { 
            this.userRepository = userRepository;
        }

        [HttpGet("All")]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<List<UserResponseDto>>> GetAll()
        {
            var listUserEntity = await userRepository.GetAllAsync();
            var listUserResponse = listUserEntity.Select(e => UserResponseDto.FromEntity(e)).ToList();
            if (listUserResponse == null)
            {
                return new NotFoundResult();
            }
            return new OkObjectResult(listUserResponse);
        }

        [HttpGet]
        [Authorize(Policy = "User")]
        public async Task<ActionResult<UserResponseDto>> GetUserByName(string name)
        {
            var user = await userRepository.GetByUsernameAsync(name);
            if (user == null)
            {
                return new NotFoundResult();
            }
            UserResponseDto u = UserResponseDto.FromEntity(user);
            return new OkObjectResult(u);
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<UserResponseDto>> CreateUser(UserRequestDto userRequestDto)
        {
            var userEntity = new User();
            userRequestDto.PopulateEntity(userEntity);
            if (!await this.userRepository.CreateAsync(userEntity)) return new BadRequestResult();
            return UserResponseDto.FromEntity(userEntity);
        }

        [HttpDelete]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var user = await this.userRepository.GetByIdAsync(id);
            if (user==null||!await this.userRepository.DeleteAsync(user)) return new BadRequestResult();
            return new OkResult();
        }

        [HttpPut]
        [Authorize(Policy = "User")]
        public async Task<ActionResult<UserResponseDto>> UpdateUserPhone(UserUpdatePhoneDto userUpdatePhoneDto)
        {
            var user = await this.userRepository.GetByIdAsync(userUpdatePhoneDto.Id);
            if (user==null) return new BadRequestResult();
            user.Phone = userUpdatePhoneDto.PhoneNumber;
            await this.userRepository.UpdateAsync(user);
            return new OkObjectResult(UserResponseDto.FromEntity(user));
        }
    }
}

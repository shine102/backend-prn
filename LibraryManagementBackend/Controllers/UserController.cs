using LibraryManagementBackend.DTO;
using LibraryManagementBackend.DTO.CommentDTO;
using LibraryManagementBackend.DTO.UserDTO;
using LibraryManagementBackend.Models;
using LibraryManagementBackend.Repositories.CommentRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;

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

        [HttpGet]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<List<UserResponseDto>>> GetAll()
        {
            var listUserEntity = await this.userRepository.GetAllAsync();
            var listUserResponse = listUserEntity.Select(e => UserResponseDto.FromEntity(e)).ToList();
            if (listUserResponse == null)
            {
                return new NotFoundResult();
            }
            return new OkObjectResult(listUserResponse);
        }

        public async Task<ActionResult<UserResponseDto>> GetUserByName(string name)
        {
            var user = await this.userRepository.GetUserByName(name);
            if (user == null)
            {
                return new NotFoundResult();
            }
            return new OkObjectResult(user);
        }

        public async Task<ActionResult<UserResponseDto>> CreateUser(UserRequestDto userRequestDto)
        {
            var userEntity = new User();
            userRequestDto.PopulateEntity(userEntity);
            if (!await this.userRepository.CreateAsync(userEntity)) return new BadRequestResult();
            return UserResponseDto.FromEntity(userEntity);
        }

        public async Task<ActionResult> DeleteUser(int id)
        {
            var user = await this.userRepository.GetByIdAsync(id);
            if (user==null||!await this.userRepository.DeleteAsync(user)) return new BadRequestResult();
            return new OkResult();
        }

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

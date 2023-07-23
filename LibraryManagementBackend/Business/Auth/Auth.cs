using LibraryManagementBackend.DTO.Auth;
using LibraryManagementBackend.Models;
using LibraryManagementBackend.Repositories.User;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibraryManagementBackend.Business.Auth
{
	public class Auth
	{
		private readonly IUserRepository userRepository;
		private readonly IConfiguration _configuration;

		public Auth(IUserRepository userRepository, IConfiguration configuration)
		{
			this.userRepository = userRepository;
			_configuration = configuration;
		}

		public async Task<User> RegisterAsync(RegisterDto u)
		{
			var existingUser = await userRepository.GetByUsernameAsync(u.Username);
			if (existingUser != null)
			{
				return null;
			}
			User user = new User();
			u.PopulateEntity(user);
			user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
			await userRepository.CreateAsync(user);
			return user;
		}

		public async Task<LoginResponseDto> LoginAsync(LoginDto u)
		{
			User user = await userRepository.GetByUsernameAsync(u.Username);
			if (user == null || !BCrypt.Net.BCrypt.Verify(u.Password, user.Password))
			{
				return null;
			}
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_configuration["JWT:SecretKey"]);
			String role = (bool)user.IsAdmin ? "Admin" : "Student";
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim(ClaimTypes.Name, user.Username),
					new Claim(ClaimTypes.Role, role)
				}),
				IssuedAt = DateTime.UtcNow,
				Issuer = _configuration["JWT:Issuer"],
				Audience = _configuration["JWT:Audience"],
				Expires = DateTime.UtcNow.AddMinutes(180),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);

			return new LoginResponseDto
			{
				Token = tokenHandler.WriteToken(token),
				Username = user.Username,
				Role = role,
				Phone = user.Phone,
				CredentialCode = user.CredentialCode
			};

		}
	}
}

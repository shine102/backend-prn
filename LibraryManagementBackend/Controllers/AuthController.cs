using LibraryManagementBackend.Business.Auth;
using LibraryManagementBackend.DTO.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementBackend.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly Auth _authService;

		[HttpPost("login")]
		[AllowAnonymous]
		public async Task<IActionResult> login(LoginDto authDTO)
		{
			var user = await _authService.LoginAsync(authDTO);
			if (user == null)
			{
				return BadRequest();
			}
			return Ok(user);
		}

		[Authorize(Policy = "Admin")]
		[HttpPost("register")]
		public async Task<IActionResult> register(RegisterDto user)
		{
			var newUser = await _authService.RegisterAsync(user);
			if (newUser == null)
			{
				return BadRequest();
			}
			return Ok(newUser);
		}
	}
}

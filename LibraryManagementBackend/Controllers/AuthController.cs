using LibraryManagementBackend.Business.Auth;
using LibraryManagementBackend.DTO.Auth;
using LibraryManagementBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementBackend.Controllers
{
	[Route("Api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly AuthService _authService;
		public AuthController(AuthService authService)
		{
			_authService = authService;
		}

		[HttpPost("Login")]
		[AllowAnonymous]
		public async Task<IActionResult> Login(LoginDto authDTO)
		{
			var user = await _authService.LoginAsync(authDTO);
			if (user == null)
			{
				return BadRequest();
			}
			return Ok(user);
		}

		[Authorize(Policy = "Admin")]
		[HttpPost("Register")]
		public async Task<IActionResult> Register(RegisterDto user)
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

using LibraryManagementBackend.Business.ChatService;
using LibraryManagementBackend.DTO.Chat;
using LibraryManagementBackend.Models;
using LibraryManagementBackend.Repositories.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibraryManagementBackend.Controllers
{
	[Route("Api/[controller]")]
	[ApiController]
	public class ChatController : ControllerBase
	{
		private readonly IChatService _chatService;
		private readonly HttpContext _httpContext;
		private readonly IUserRepository _user;
		public ChatController(IChatService chatService, IHttpContextAccessor httpContextAccessor, IUserRepository user)
		{
			_chatService = chatService;
			_httpContext = httpContextAccessor.HttpContext;
			_user = user;
		}

		[HttpPost("LoadChat")]
		[Authorize(Policy = "User")]
		public async Task<IActionResult> LoadChat(GetChatDTO getChat)
		{
			// access username from token
			var username = _httpContext.User.FindFirst(ClaimTypes.Name)?.Value;

			// check username from token and username from request
			if (username != null)
			{
				User u = await _user.GetByUsernameAsync(username);
				// if not match, return bad request
				if (getChat.UserId != u.Id)
				{
					return BadRequest();
				}
				else
				{
					IEnumerable<ChatResponseDTO> listChat = _chatService.LoadChat(getChat);
					if (listChat == null)
					{
						return NotFound();
					}
					return Ok(listChat);
				}
			}
			else
			{
				return BadRequest();
			}
		}

		[HttpPost("LoadMessage")]
		[Authorize(Policy = "User")]
		public async Task<IActionResult> LoadMessage([FromBody] GetMessageDTO getMessage)
		{
			// access username from token
			var username = _httpContext.User.FindFirst(ClaimTypes.Name)?.Value;
			// check username from token and username from request
			if (username != null)
			{
				User u = await _user.GetByUsernameAsync(username);
				// if not match, return bad request
				if (getMessage.OwnerId != u.Id || getMessage.OwnerId == getMessage.PartnerId)
				{
					return BadRequest();
				}
				else
				{
					IEnumerable<MessageResponseDTO> listMessage = _chatService.LoadMessage(getMessage);
					if (listMessage == null)
					{
						return NotFound();
					}
					return Ok(listMessage);
				}
			}
			else
			{
				return BadRequest();
			}
		}

		[HttpPost("SendMessage")]
		[Authorize(Policy = "User")]
		public async Task<IActionResult> SendMessage(SendMessageDTO sendMessage)
		{
			// access username from token
			var username = _httpContext.User.FindFirst(ClaimTypes.Name)?.Value;
			// check username from token and username from request
			if (username != null)
			{
				User u = await _user.GetByUsernameAsync(username);
				// if not match, return bad request
				if (sendMessage.sender_id != u.Id)
				{
					return BadRequest();
				}
				else
				{
					if (await _chatService.SendMessage(sendMessage))
					{
						return Ok();
					}
					else
					{
						return BadRequest();
					}
				}
			}
			else
			{
				return BadRequest();
			}
		}
	}
}

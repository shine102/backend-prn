namespace LibraryManagementBackend.Hubs
{
	using Microsoft.AspNetCore.SignalR;
	public class ChatHub : Hub
	{

		public async Task SendMessage(string userId, string partnerId, string message, string chatId)
		{
			await Clients.Group(chatId).SendAsync("ReceiveMessage", userId, message);
		}

		public async Task JoinPrivateChat(string chatId)
		{
			await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
		}

		public async Task LeavePrivateChat(string chatId)
		{
			await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId);
		}
	}
}

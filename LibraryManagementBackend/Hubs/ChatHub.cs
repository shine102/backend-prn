namespace LibraryManagementBackend.Hubs
{
	using Microsoft.AspNetCore.SignalR;
	public class ChatHub : Hub
	{
		public async Task SendMessage(string user, string message)
		{
			await Clients.Group(GetGroupName()).SendAsync("ReceiveMessage", user, message);
		}

		public async Task JoinPrivateChat(string userId)
		{
			await Groups.AddToGroupAsync(Context.ConnectionId, GetGroupName(userId));
		}

		public async Task LeavePrivateChat(string userId)
		{
			await Groups.RemoveFromGroupAsync(Context.ConnectionId, GetGroupName(userId));
		}

		private string GetGroupName(string userId = "")
		{
			return "123";
			var currentUserId = userId != "" ? userId : Context.UserIdentifier;
			var otherUserId = Context.UserIdentifier == "User1" ? "User2" : "User1";
			return $"{currentUserId}-{otherUserId}";
		}
	}
}

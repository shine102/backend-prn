namespace LibraryManagementBackend.Hubs
{
    using LibraryManagementBackend.Business.ChatService;
    using Microsoft.AspNetCore.SignalR;

    public class ChatHub : Hub
    {
        private readonly IChatService chatService;

        public ChatHub(IChatService chatService)
        {
            this.chatService = chatService;
        }

        public async Task SendMessage(string userId, string partnerId, string message, string chatId)
        {
            await this.chatService.SendMessage(new()
            {
                sender_id   = int.Parse(userId),
                receiver_id = int.Parse(partnerId),
                content     = message,
            });
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
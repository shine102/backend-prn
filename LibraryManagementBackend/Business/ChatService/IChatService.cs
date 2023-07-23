using LibraryManagementBackend.DTO.Chat;

namespace LibraryManagementBackend.Business.ChatService
{
	public interface IChatService
	{
		public Task<bool> SendMessage(SendMessageDTO sendMessage);
		public IEnumerable<MessageResponseDTO> LoadMessage(GetMessageDTO getMessage);
		public IEnumerable<ChatResponseDTO> LoadChat(GetChatDTO conversationDTO);
	}
}

namespace LibraryManagementBackend.DTO.Chat
{
	public class ChatResponseDTO
	{
		public string partner_username { get; set; }
		public int partner_id { get; set; }
		public string lastMessage { get; set; }
		public int chatID { get; set; }
		public ChatResponseDTO() { }
	}
}

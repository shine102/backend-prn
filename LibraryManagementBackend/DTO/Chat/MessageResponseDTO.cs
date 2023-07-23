namespace LibraryManagementBackend.DTO.Chat
{
	public class MessageResponseDTO
	{
		public string sender_name { get; set; }
		public string content { get; set; }
		public string receiver_name { get; set; }
		public MessageResponseDTO() { }
	}
}

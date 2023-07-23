namespace LibraryManagementBackend.DTO.Chat
{
	public class SendMessageDTO
	{
		public int sender_id { get; set; }
		public int receiver_id { get; set; }
		public string content { get; set; }
		public SendMessageDTO() { }
	}
}

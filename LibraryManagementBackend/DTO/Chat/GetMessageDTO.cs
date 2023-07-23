namespace LibraryManagementBackend.DTO.Chat
{
	public class GetMessageDTO
	{
		public int ChatId { get; set; }
		public int OwnerId { get; set; }
		public int PartnerId { get; set; }
		public GetMessageDTO()
		{
		}
	}
}

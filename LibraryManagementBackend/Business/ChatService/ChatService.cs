using LibraryManagementBackend.DTO.Chat;
using LibraryManagementBackend.Models;

namespace LibraryManagementBackend.Business.ChatService
{
	public class ChatService : IChatService
	{
		private readonly LibraryManagementDbContext _context;
		public ChatService(LibraryManagementDbContext context)
		{
			_context = context;
		}

		/*
		 * Get all chat of user
		 * GetChatDTO: userId
		 */
		public IEnumerable<ChatResponseDTO> LoadChat(GetChatDTO conversationDTO)
		{
			List<Participant> Participants = _context.Participants.Where(p => p.UserId == conversationDTO.UserId).ToList();
			List<ChatResponseDTO> chatResponseDTOs = new();
			if (Participants.Count == 0)
			{
				return null;
			} else
			{
				foreach(Participant p in Participants)
				{
					int partner_id = _context.Participants.Where(o => o.ChatId == p.ChatId && o.UserId != conversationDTO.UserId).FirstOrDefault().UserId;
					User partner = _context.Users.Find(partner_id);
					Chat chat = _context.Chats.Find(p.ChatId);
					if(partner == null)
					{
						break;
					}
					chatResponseDTOs.Add(new()
					{
						partner_id = partner.Id,
						partner_username = partner.Username,
						lastMessage = chat.LastMessage,
						chatID = p.ChatId
					}
					);
				}
				return chatResponseDTOs;
			}

		}
		/*
		 * Get all message of chat
		 * GetMessageDTO : chatId, ownerId, partnerId
		 */

		public IEnumerable<MessageResponseDTO> LoadMessage(GetMessageDTO getMessage)
		{
			Participant p = _context.Participants.Where(p => p.ChatId == getMessage.ChatId).Where(p => p.UserId == getMessage.OwnerId).FirstOrDefault();
			if (p != null)
			{
				List<Message> messages = _context.Messages.Where(m => m.ChatId == getMessage.ChatId).ToList();
				List<MessageResponseDTO> messageResponseDTOs = new();
				if (messages.Count > 0)
				{
					foreach(Message m in messages)
					{
						User sender = _context.Users.Find(m.UserId);

						messageResponseDTOs.Add(new()
						{
							sender_name = sender.Username,
							content = m.Content,
							receiver_name = m.UserId == getMessage.OwnerId ? _context.Users.Where(u => u.Id == getMessage.PartnerId).FirstOrDefault().Username : _context.Users.Where(u => u.Id == getMessage.OwnerId).FirstOrDefault().Username
						});
					}
					return messageResponseDTOs;
				}
				else
				{
					return null;
				}
			}
			else
			{
				return null;
			}
		}

		public async Task<bool> SendMessage(SendMessageDTO sendMessage)
		{
			if (sendMessage.sender_id == sendMessage.receiver_id)
			{
				return false;
			}
			var commonChat = _context.Participants
							.Where(p => p.UserId == sendMessage.sender_id)
							.Join(_context.Participants.Where(p => p.UserId == sendMessage.receiver_id),
								p1 => p1.ChatId,
								p2 => p2.ChatId,
								(p1, p2) => p1.ChatId)
							.FirstOrDefault();
			if (commonChat != null)
			{
				_context.Messages.Add(new Message()
				{
					ChatId = commonChat,
					Content = sendMessage.content,
					UserId = sendMessage.sender_id
				});

				Chat chat = _context.Chats.FirstOrDefault(c => c.Id == commonChat);
				if (chat != null)
				{
					chat.LastMessage = sendMessage.content;
				}
				await _context.SaveChangesAsync();
				return true;
			}
			else
			{
				try
				{
					Chat newChat = new()
					{
						LastMessage = sendMessage.content
					};
					_context.Chats.Add(newChat);
					await _context.SaveChangesAsync();

					Participant Participant1 = new()
					{
						ChatId = newChat.Id,
						UserId = sendMessage.sender_id
					};

					await _context.Participants.AddAsync(Participant1);

					Participant Participant2 = new()
					{
						ChatId = newChat.Id,
						UserId = sendMessage.receiver_id
					};
					await _context.Participants.AddAsync(Participant2);

					Message message = new()
					{
						ChatId = newChat.Id,
						Content = sendMessage.content,
						UserId = sendMessage.sender_id
					};
					await _context.Messages.AddAsync(message);

					await _context.SaveChangesAsync();

					return true;
				} catch(Exception e)
				{
					Console.WriteLine(e.Message);
					return false;
				}
			}
		}
	}
}

using LibraryManagementBackend.Models;

namespace LibraryManagementBackend.DTO.CommentDTO
{
    public class CommentResponseDto : IResponseDto<Comment, CommentResponseDto>
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int BookId { get; set; }
        public string Username { get; set; }
        public static CommentResponseDto FromEntity(Comment entity)
        {
            return new()
            {
                Id = entity.Id,
                Content = entity.Content,
                BookId = entity.BookId,
                Username = entity.User.Username
            };
        }
    }
}

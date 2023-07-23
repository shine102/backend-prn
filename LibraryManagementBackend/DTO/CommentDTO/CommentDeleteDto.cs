using LibraryManagementBackend.Models;

namespace LibraryManagementBackend.DTO.CommentDTO
{
    public class CommentDeleteDto : IRequestDto<Comment>
    {
        public int Id { get; set; }
        public void PopulateEntity(Comment entity)
        {
           this.Id = entity.Id;
        }
    }
}
using LibraryManagementBackend.DTO.CommentDTO;
using LibraryManagementBackend.Models;

namespace LibraryManagementBackend.Repositories.CommentRepo
{
    public interface ICommentRepository: IRepository<Comment>
    {
        public Task<List<CommentResponseDto>> GetAllCommentOfBook(int BookID);

        public Task<bool> CreateCommentAsync(CommentCreateDto commentCreateDto);

        public Task<bool> DeleteComment(CommentDeleteDto commentDeleteDto);
    }
}

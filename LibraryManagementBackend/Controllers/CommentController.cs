using LibraryManagementBackend.DTO;
using LibraryManagementBackend.DTO.Category;
using LibraryManagementBackend.DTO.CommentDTO;
using LibraryManagementBackend.Models;
using LibraryManagementBackend.Repositories.CommentRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace LibraryManagementBackend.Controllers
{
    [ApiController]
    [Route("Api/[Controller]")]
    public class CommentController
    {
        private readonly CommentRepository commentRepository;
        public CommentController(CommentRepository commentRepository)
        {
            this.commentRepository = commentRepository;
        }
        [HttpGet]
        [Authorize(Policy = "User")]
        public async Task<ActionResult<List<CommentResponseDto>>> Get(int bookID)
        {
            List<CommentResponseDto> comments = await commentRepository.GetAllCommentOfBook(bookID);
            if (comments == null)
            {
                return new NotFoundResult();
            }
            return new OkObjectResult(comments);
        }

        [HttpPost]
        [Authorize(Policy = "User")]
        public async Task<ActionResult<CommentResponseDto>> Post(CommentCreateDto comment)
        {
            var commentEntity = new Comment();
            comment.Populate(commentEntity);
            bool result = await commentRepository.CreateCommentAsync(comment);
            if (!result)
            {
                return new BadRequestResult();
            }
            return CommentResponseDto.FromEntity(commentEntity);
        }

        [HttpDelete]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Delete(CommentDeleteDto comment)
        {
            await this.commentRepository.DeleteComment(comment);
            return new OkResult();
        }
    }
}

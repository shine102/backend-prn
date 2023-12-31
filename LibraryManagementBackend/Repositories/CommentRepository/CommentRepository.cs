﻿using LibraryManagementBackend.DTO.CommentDTO;
using LibraryManagementBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementBackend.Repositories.CommentRepo
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        private LibraryManagementDbContext context;
        public CommentRepository(LibraryManagementDbContext context) : base(context)
        {
            this.context = context;
        }
        protected override DbSet<Comment> DbSet => this.context.Comments;

        public Task<List<CommentResponseDto>> GetAllCommentOfBook(int BookID)
        {
            List<CommentResponseDto> commentData = this.context.Comments.Where(cmt=>cmt.BookId == BookID).Select(cmt=>new CommentResponseDto() { Id = cmt.Id,Content = cmt.Content,Username = cmt.User.Username,BookId = cmt.BookId}).ToList();
            return Task.FromResult(commentData);
        }

        public Task<bool> CreateCommentAsync(CommentCreateDto commentCreateDto)
        {
            if(!this.context.Books.Any(e=>e.Id==commentCreateDto.BookId) || !this.context.Users.Any(e=>e.Username==commentCreateDto.Username)) {
                return Task.FromResult(false);
            }
            Comment comment = new() { Content = commentCreateDto.Content, BookId = commentCreateDto.BookId, UserId = this.context.Users.Where(user => user.Username.Equals(commentCreateDto.Username)).First().Id };
            return this.CreateAsync(comment);
        }

        public Task<bool> DeleteComment(CommentDeleteDto commentDeleteDto)
        {
            var comment = this.context.Comments.FirstOrDefault(cmt=>cmt.Id.Equals(commentDeleteDto.Id));
            if(comment == null) return Task.FromResult(false);
            return this.DeleteAsync(comment);
        }

        
    }
}

﻿using LibraryManagementBackend.Models;

namespace LibraryManagementBackend.DTO.CommentDTO
{
    public class CommentCreateDto : IRequestDto<Comment>
    {
        public string Content { get; set; }
        public int BookId { get; set; }
        public string Username { get; set; }
        public void PopulateEntity(Comment entity)
        {
            entity.Content = Content;
            entity.BookId = BookId;
            entity.User.Username = Username;
        }
    }
}
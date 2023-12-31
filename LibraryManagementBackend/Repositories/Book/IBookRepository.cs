﻿namespace LibraryManagementBackend.Repositories.Book
{
    using LibraryManagementBackend.Models;

    public interface IBookRepository : IRepository<Book>
    {
        public Task<IEnumerable<Book>> SearchAsync(string? title = null, string? author = null, int? categoryId = null, int? skip = null, int? take = null);

        public Task<int> CountAsync(string? title = null, string? author = null, int? categoryId = null);
    }
}
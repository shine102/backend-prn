namespace LibraryManagementBackend.Repositories.Book
{
    using LibraryManagementBackend.Models;

    public interface IBookRepository : IRepository<Book>
    {
        public Task<IEnumerable<Book>> Find(string? title, string? author, int? categoryId);
    }
}
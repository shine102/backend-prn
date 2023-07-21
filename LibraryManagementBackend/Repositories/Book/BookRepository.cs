namespace LibraryManagementBackend.Repositories.Book
{
    using LibraryManagementBackend.Models;
    using Microsoft.EntityFrameworkCore;

    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(LibraryManagementDbContext context) : base(context)
        {
        }

        protected override DbSet<Book> DbSet => this.context.Books;

        public async Task<IEnumerable<Book>> Search(string? title = null, string? author = null, int? categoryId = null, int? skip = null, int? take = null)
        {
            var books = this.DbSet.AsQueryable();
            if (title is not null)
            {
                books = books.Where(book => book.Title.Contains(title));
            }
            if (author is not null)
            {
                books = books.Where(book => book.Author.Contains(author));
            }
            if (categoryId is not null)
            {
                books = books.Where(book => book.CategoryId == categoryId);
            }
            if (skip is not null)
            {
                books = books.Skip(skip.Value);
            }
            if (take is not null)
            {
                books = books.Take(take.Value);
            }
            return await books.ToListAsync();
        }
    }
}
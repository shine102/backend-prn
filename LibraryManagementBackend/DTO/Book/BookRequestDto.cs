namespace LibraryManagementBackend.DTO.Book;

using LibraryManagementBackend.Models;

public class BookRequestDto : IRequestDto<Book>
{
    public string Title      { get; init; }
    public string Image      { get; init; }
    public string Author     { get; init; }
    public string Content    { get; init; }
    public int    CategoryId { get; init; }

    public Book ToEntity()
    {
        return new()
        {
            Title      = this.Title,
            Image      = this.Image,
            Author     = this.Author,
            Content    = this.Content,
            CategoryId = this.CategoryId,
        };
    }
}
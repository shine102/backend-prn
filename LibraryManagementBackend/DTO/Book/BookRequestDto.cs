namespace LibraryManagementBackend.DTO.Book;

using LibraryManagementBackend.Models;

public class BookRequestDto : IRequestDto<Book>
{
    public string Title      { get; init; }
    public string Image      { get; init; }
    public string Author     { get; init; }
    public string Content    { get; init; }
    public int    CategoryId { get; init; }

    public void Populate(Book entity)
    {
        entity.Title      = this.Title;
        entity.Image      = this.Image;
        entity.Author     = this.Author;
        entity.Content    = this.Content;
        entity.CategoryId = this.CategoryId;
    }
}
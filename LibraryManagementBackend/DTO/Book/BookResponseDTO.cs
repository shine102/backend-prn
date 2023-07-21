namespace LibraryManagementBackend.DTO.Book;

using LibraryManagementBackend.Models;

public class BookResponseDto : IResponseDto<Book, BookResponseDto>
{
    public int    Id         { get; init; }
    public string Title      { get; init; }
    public string Image      { get; init; }
    public string Author     { get; init; }
    public string Content    { get; init; }
    public int    CategoryId { get; init; }

    public static BookResponseDto FromEntity(Book book)
    {
        return new()
        {
            Id         = book.Id,
            Title      = book.Title,
            Image      = book.Image,
            Author     = book.Author,
            Content    = book.Content,
            CategoryId = book.CategoryId,
        };
    }
}
namespace LibraryManagementBackend.DTO.Book;

using System.ComponentModel.DataAnnotations;
using LibraryManagementBackend.Models;

public class BookRequestDto : IRequestDto<Book>, IValidatableObject
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

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(this.Title))
        {
            yield return new("Title is required");
        }
        if (string.IsNullOrWhiteSpace(this.Image))
        {
            yield return new("Image is required");
        }
        if (string.IsNullOrWhiteSpace(this.Author))
        {
            yield return new("Author is required");
        }
        if (string.IsNullOrWhiteSpace(this.Content))
        {
            yield return new("Content is required");
        }
    }
}
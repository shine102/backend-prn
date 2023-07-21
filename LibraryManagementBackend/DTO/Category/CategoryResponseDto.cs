namespace LibraryManagementBackend.DTO.Category;

using LibraryManagementBackend.Models;

public class CategoryResponseDto : IResponseDto<Category, CategoryResponseDto>
{
    public int    Id   { get; init; }
    public string Name { get; init; }

    public static CategoryResponseDto FromEntity(Category category)
    {
        return new()
        {
            Id   = category.Id,
            Name = category.Name,
        };
    }
}
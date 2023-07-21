namespace LibraryManagementBackend.DTO.Category;

using LibraryManagementBackend.Models;

public class CategoryRequestDto : IRequestDto<Category>
{
    public string Name { get; init; }

    public Category ToEntity()
    {
        return new()
        {
            Name = this.Name,
        };
    }
}
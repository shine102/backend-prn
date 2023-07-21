namespace LibraryManagementBackend.DTO.Category;

using LibraryManagementBackend.Models;

public class CategoryRequestDto : IRequestDto<Category>
{
    public string Name { get; init; }

    public void Populate(Category entity)
    {
        entity.Name = this.Name;
    }
}
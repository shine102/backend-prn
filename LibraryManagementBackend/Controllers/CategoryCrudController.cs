namespace LibraryManagementBackend.Controllers;

using LibraryManagementBackend.DTO.Category;
using LibraryManagementBackend.Models;
using LibraryManagementBackend.Repositories.Category;

public class CategoryCrudController : CrudController<Category, CategoryRequestDto, CategoryResponseDto>
{
    public CategoryCrudController(ICategoryRepository repository) : base(repository)
    {
    }
}
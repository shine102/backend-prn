namespace LibraryManagementBackend.Controllers;

using LibraryManagementBackend.DTO.Category;
using LibraryManagementBackend.Models;
using LibraryManagementBackend.Repositories.Category;

public class CategoryController : CrudController<Category, CategoryRequestDto, CategoryResponseDto>
{
    public CategoryController(ICategoryRepository repository) : base(repository)
    {
    }
}
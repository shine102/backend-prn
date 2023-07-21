namespace LibraryManagementBackend.Controllers;

using LibraryManagementBackend.DTO.Book;
using LibraryManagementBackend.Errors;
using LibraryManagementBackend.Models;
using LibraryManagementBackend.Repositories.Book;
using LibraryManagementBackend.Repositories.Category;
using Microsoft.AspNetCore.Mvc;

public class BookCrudController : CrudController<Book, BookRequestDto, BookResponseDto>
{
    private readonly ICategoryRepository categoryRepository;

    public BookCrudController(IBookRepository repository, ICategoryRepository categoryRepository) : base(repository)
    {
        this.categoryRepository = categoryRepository;
    }

    public override async Task<ActionResult<BookResponseDto>> Create(BookRequestDto dto)
    {
        if (await this.categoryRepository.GetByIdAsync(dto.CategoryId) is null)
        {
            return this.BadRequest(new Error { Message = "Category not found" });
        }
        return await base.Create(dto);
    }
}
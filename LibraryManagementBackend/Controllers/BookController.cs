namespace LibraryManagementBackend.Controllers;

using LibraryManagementBackend.DTO.Book;
using LibraryManagementBackend.Errors;
using LibraryManagementBackend.Models;
using LibraryManagementBackend.Repositories.Book;
using LibraryManagementBackend.Repositories.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

public class BookController : CrudController<Book, BookRequestDto, BookResponseDto>
{
    private readonly IBookRepository     repository;
    private readonly ICategoryRepository categoryRepository;

    public BookController(IBookRepository repository, ICategoryRepository categoryRepository) : base(repository)
    {
        this.repository         = repository;
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

    [HttpGet(nameof(Search))]
    [Authorize(Policy = "User")]
    public async Task<ActionResult<List<BookResponseDto>>> Search(string? title, string? author, int? categoryId)
    {
        return (await this.repository.Search(title, author, categoryId)).Select(BookResponseDto.FromEntity).ToList();
    }
}
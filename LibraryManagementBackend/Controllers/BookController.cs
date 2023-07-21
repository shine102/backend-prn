namespace LibraryManagementBackend.Controllers;

using LibraryManagementBackend.DTO;
using LibraryManagementBackend.DTO.Book;
using LibraryManagementBackend.Models;
using LibraryManagementBackend.Repositories.Book;
using LibraryManagementBackend.Repositories.Category;
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
            return this.BadRequest(new MessageDto($"Category {dto.CategoryId} not found"));
        }
        return await base.Create(dto);
    }

    // [Authorize(Policy = "User")]
    [HttpGet(nameof(Search))]
    public async Task<ActionResult<List<BookResponseDto>>> Search(string? title, string? author, int? categoryId, int? page, int? pageSize)
    {
        var books = page is not null && pageSize is not null
            ? await this.repository.Search(title, author, categoryId, (page - 1) * pageSize, pageSize)
            : await this.repository.Search(title, author, categoryId);
        return books.Select(BookResponseDto.FromEntity).ToList();
    }
}
namespace LibraryManagementBackend.Controllers;

using LibraryManagementBackend.DTO;
using LibraryManagementBackend.Models;
using LibraryManagementBackend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("Api/[Controller]")]
public abstract class CrudController<T, TRequestDto, TResponseDto> : ControllerBase
    where T : Entity, new()
    where TRequestDto : IRequestDto<T>
    where TResponseDto : IResponseDto<T, TResponseDto>
{
    private readonly IRepository<T> repository;

    protected CrudController(IRepository<T> repository)
    {
        this.repository = repository;
    }

    // [Authorize(Policy = "User")]
    [HttpGet("{id:int}")]
    public virtual async Task<ActionResult<TResponseDto>> GetById(int id)
    {
        var entity = await this.repository.GetByIdAsync(id);
        if (entity is null) return this.NotFound();
        return TResponseDto.FromEntity(entity);
    }

    // [Authorize(Policy = "User")]
    [HttpGet]
    public virtual async Task<ActionResult<List<TResponseDto>>> GetAll()
    {
        return (await this.repository.GetAllAsync()).Select(TResponseDto.FromEntity).ToList();
    }

    // [Authorize(Policy = "Admin")]
    [HttpPost]
    public virtual async Task<ActionResult<TResponseDto>> Create(TRequestDto dto)
    {
        var entity = dto.ToEntity();
        if (!await this.repository.CreateAsync(entity)) return this.BadRequest();
        return TResponseDto.FromEntity(entity);
    }

    // [Authorize(Policy = "Admin")]
    [HttpPut("{id:int}")]
    public virtual async Task<ActionResult> Update(int id, TRequestDto dto)
    {
        var entity = dto.ToEntity();
        entity.Id = id;
        if (!await this.repository.UpdateAsync(entity)) return this.NotFound();
        return this.Ok();
    }

    // [Authorize(Policy = "Admin")]
    [HttpDelete("{id:int}")]
    public virtual async Task<ActionResult> Delete(int id)
    {
        var entity = new T { Id = id };
        if (!await this.repository.DeleteAsync(entity)) return this.NotFound();
        return this.Ok();
    }
}
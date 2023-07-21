namespace LibraryManagementBackend.Controllers;

using LibraryManagementBackend.DTO;
using LibraryManagementBackend.Models;
using LibraryManagementBackend.Repositories;
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
        var entity = new T();
        dto.Populate(entity);
        if (!await this.repository.CreateAsync(entity)) return this.BadRequest();
        return TResponseDto.FromEntity(entity);
    }

    // [Authorize(Policy = "Admin")]
    [HttpPut("{id:int}")]
    public virtual async Task<ActionResult<MessageDto>> Update(int id, TRequestDto dto)
    {
        var entity = await this.repository.GetByIdAsync(id);
        if (entity is null) return this.NotFound(id);
        dto.Populate(entity);
        if (!await this.repository.UpdateAsync(entity)) return this.BadRequest();
        return new MessageDto($"{typeof(T).Name} {id} updated");
    }

    // [Authorize(Policy = "Admin")]
    [HttpDelete("{id:int}")]
    public virtual async Task<ActionResult<MessageDto>> Delete(int id)
    {
        var entity = await this.repository.GetByIdAsync(id);
        if (entity is null) return this.NotFound(id);
        if (!await this.repository.DeleteAsync(entity)) return this.BadRequest();
        return new MessageDto($"{typeof(T).Name} {id} deleted");
    }

    protected virtual NotFoundObjectResult NotFound(int id)
    {
        return base.NotFound(new MessageDto($"{typeof(T).Name} {id} not found"));
    }

    protected new virtual BadRequestObjectResult BadRequest()
    {
        return base.BadRequest(new MessageDto("Invalid request"));
    }
}
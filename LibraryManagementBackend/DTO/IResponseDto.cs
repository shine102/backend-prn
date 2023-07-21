namespace LibraryManagementBackend.DTO;

using LibraryManagementBackend.Models;

public interface IResponseDto<in T, out TResponseDto> where T : Entity
{
    public static abstract TResponseDto FromEntity(T entity);
}
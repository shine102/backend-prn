namespace LibraryManagementBackend.DTO;

using LibraryManagementBackend.Models;

public interface IRequestDto<out T> where T : Entity
{
    public T ToEntity();
}
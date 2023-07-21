namespace LibraryManagementBackend.DTO;

using LibraryManagementBackend.Models;

public interface IRequestDto<in T> where T : Entity
{
    public void Populate(T entity);
}
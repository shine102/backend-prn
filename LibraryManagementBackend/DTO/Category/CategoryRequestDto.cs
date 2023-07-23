namespace LibraryManagementBackend.DTO.Category;

using System.ComponentModel.DataAnnotations;
using LibraryManagementBackend.Models;

public class CategoryRequestDto : IRequestDto<Category>, IValidatableObject
{
    public string Name { get; init; }

    public void PopulateEntity(Category entity)
    {
        entity.Name = this.Name;
    }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(this.Name))
        {
            yield return new("Name is required");
        }
    }
}
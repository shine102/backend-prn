namespace LibraryManagementBackend.Models;

public class Category : Entity
{
    public string Name { get; set; }

    public virtual ICollection<Book> Books { get; set; }
}
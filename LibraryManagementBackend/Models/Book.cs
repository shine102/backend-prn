namespace LibraryManagementBackend.Models;

public class Book : Entity
{
    public string Title      { get; set; }
    public string Image      { get; set; }
    public string Author     { get; set; }
    public string Content    { get; set; }
    public int    CategoryId { get; set; }

    public virtual Category             Category { get; set; }
    public virtual ICollection<Comment> Comments { get; set; }
}
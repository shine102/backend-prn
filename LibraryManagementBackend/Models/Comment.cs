namespace LibraryManagementBackend.Models;

public class Comment : Entity
{
    public string Content { get; set; }
    public int    UserId  { get; set; }
    public int    BookId  { get; set; }

    public virtual User User { get; set; }
    public virtual Book Book { get; set; }
}
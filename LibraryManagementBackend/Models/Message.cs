namespace LibraryManagementBackend.Models;

public class Message : Entity
{
    public int    ChatId  { get; set; }
    public int    UserId  { get; set; }
    public string Content { get; set; }

    public virtual Chat Chat { get; set; }
    public virtual User User { get; set; }
}
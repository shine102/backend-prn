namespace LibraryManagementBackend.Models;

public class Participant : Entity
{
    public int ChatId { get; set; }
    public int UserId { get; set; }

    public virtual Chat Chat { get; set; }
    public virtual User User { get; set; }
}
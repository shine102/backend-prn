namespace LibraryManagementBackend.Models;

public class Chat : Entity
{
    public string LastMessage { get; set; }

    public virtual ICollection<Message>     Messages     { get; set; }
    public virtual ICollection<Participant> Participants { get; set; }
}
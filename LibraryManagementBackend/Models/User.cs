namespace LibraryManagementBackend.Models;

public class User : Entity
{
    public string Username       { get; set; }
    public string Password       { get; set; }
    public bool   IsAdmin        { get; set; } = false;
    public string Phone          { get; set; }
    public string CredentialCode { get; set; }

    public virtual ICollection<Comment>     Comments     { get; set; }
    public virtual ICollection<Message>     Messages     { get; set; }
    public virtual ICollection<Participant> Participants { get; set; }
}
namespace LibraryManagementBackend.DTO;

public class MessageDto
{
    public string Message { get; }

    public MessageDto(string message)
    {
        this.Message = message;
    }
}
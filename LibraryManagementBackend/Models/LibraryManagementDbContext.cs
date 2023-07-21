namespace LibraryManagementBackend.Models;

using Microsoft.EntityFrameworkCore;

public class LibraryManagementDbContext : DbContext
{
    public DbSet<Book>        Books        { get; set; }
    public DbSet<Category>    Categories   { get; set; }
    public DbSet<Chat>        Chats        { get; set; }
    public DbSet<Comment>     Comments     { get; set; }
    public DbSet<Message>     Messages     { get; set; }
    public DbSet<Participant> Participants { get; set; }
    public DbSet<User>        Users        { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json")
                            .Build();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("Connection"), sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure();
        });
    }
}
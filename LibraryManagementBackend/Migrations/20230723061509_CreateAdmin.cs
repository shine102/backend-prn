using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagementBackend.Migrations
{
    /// <inheritdoc />
    public partial class CreateAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CredentialCode", "IsAdmin", "Password", "Phone", "Username" },
                values: new object[] { -1, "", true, "$2a$12$yrV/MXrEkAGn51voH2jgpevQbLulaj3GyfG4KUZSJh83a34mJny16", "", "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -1);
        }
    }
}

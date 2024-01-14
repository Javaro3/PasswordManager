using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class deleteUserNameField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "PasswordInfos");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "PasswordInfos",
                newName: "Login");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Login",
                table: "PasswordInfos",
                newName: "UserName");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "PasswordInfos",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}

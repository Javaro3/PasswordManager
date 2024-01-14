using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class test1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Key",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "PasswordInfos",
                newName: "Password");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "PasswordInfos",
                newName: "PasswordHash");

            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}

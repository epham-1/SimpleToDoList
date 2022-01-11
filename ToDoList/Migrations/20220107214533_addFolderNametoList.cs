using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoList.Migrations
{
    public partial class addFolderNametoList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FolderName",
                table: "Lists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FolderName",
                table: "Lists");
        }
    }
}

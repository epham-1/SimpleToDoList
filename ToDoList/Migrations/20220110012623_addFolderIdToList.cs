﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoList.Migrations
{
    public partial class addFolderIdToList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FolderName",
                table: "Lists");

            migrationBuilder.AddColumn<int>(
                name: "FolderId",
                table: "Lists",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FolderId",
                table: "Lists");

            migrationBuilder.AddColumn<string>(
                name: "FolderName",
                table: "Lists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

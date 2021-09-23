using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinAPIDemo.Data.Migrations
{
    public partial class UsersEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Token = table.Column<string>(type: "TEXT", nullable: true),
                    TokenExpiration = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
                // add test data
                migrationBuilder.InsertData(
                    table: "Users",
                    columns: new[] { "Id", "Username", "Password" },
                    values: new object[] { Guid.NewGuid().ToString(), "admin", "password" });                
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

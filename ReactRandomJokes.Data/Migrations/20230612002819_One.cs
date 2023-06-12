using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReactRandomJokes.Data.Migrations
{
    public partial class One : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "JokeId",
                table: "Jokes",
                newName: "OriginId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OriginId",
                table: "Jokes",
                newName: "JokeId");
        }
    }
}

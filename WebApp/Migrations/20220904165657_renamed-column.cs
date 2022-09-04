using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AzureTest.Migrations
{
    public partial class renamedcolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Tests",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Tests",
                newName: "Type");
        }
    }
}

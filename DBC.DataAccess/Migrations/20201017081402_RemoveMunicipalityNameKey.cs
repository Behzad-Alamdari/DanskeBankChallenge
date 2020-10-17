using Microsoft.EntityFrameworkCore.Migrations;

namespace DBC.DataAccess.Migrations
{
    public partial class RemoveMunicipalityNameKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Municipalities_Name",
                table: "Municipalities");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Municipalities",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Municipalities",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Municipalities_Name",
                table: "Municipalities",
                column: "Name");
        }
    }
}

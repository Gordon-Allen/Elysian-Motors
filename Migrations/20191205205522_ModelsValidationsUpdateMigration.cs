using Microsoft.EntityFrameworkCore.Migrations;

namespace ElysianMotors.Migrations
{
    public partial class ModelsValidationsUpdateMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Vehicles",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Vehicles",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}

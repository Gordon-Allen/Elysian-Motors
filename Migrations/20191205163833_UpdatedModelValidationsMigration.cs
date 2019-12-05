using Microsoft.EntityFrameworkCore.Migrations;

namespace ElysianMotors.Migrations
{
    public partial class UpdatedModelValidationsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Img",
                table: "Vehicles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Img",
                table: "Vehicles",
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}

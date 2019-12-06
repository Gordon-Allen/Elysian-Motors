using Microsoft.EntityFrameworkCore.Migrations;

namespace ElysianMotors.Migrations
{
    public partial class ModelsValidationsUpdate2Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PurchasePrice",
                table: "Orders",
                nullable: true,
                oldClrType: typeof(double));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "PurchasePrice",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}

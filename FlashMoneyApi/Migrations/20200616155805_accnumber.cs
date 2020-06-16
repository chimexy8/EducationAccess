using Microsoft.EntityFrameworkCore.Migrations;

namespace FlashMoneyApi.Migrations
{
    public partial class accnumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccountNumber",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BankName",
                table: "User",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountNumber",
                table: "User");

            migrationBuilder.DropColumn(
                name: "BankName",
                table: "User");
        }
    }
}

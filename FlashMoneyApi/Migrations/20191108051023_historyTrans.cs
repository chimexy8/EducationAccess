using Microsoft.EntityFrameworkCore.Migrations;

namespace FlashMoneyApi.Migrations
{
    public partial class historyTrans : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DestinationPhone",
                table: "TransactionHistory",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "TransactionHistory",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Receipient",
                table: "TransactionHistory",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DestinationPhone",
                table: "TransactionHistory");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "TransactionHistory");

            migrationBuilder.DropColumn(
                name: "Receipient",
                table: "TransactionHistory");
        }
    }
}

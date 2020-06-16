using Microsoft.EntityFrameworkCore.Migrations;

namespace FlashMoneyApi.Migrations
{
    public partial class historypassport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReceipientPassport",
                table: "TransactionHistory",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceipientPassport",
                table: "TransactionHistory");
        }
    }
}

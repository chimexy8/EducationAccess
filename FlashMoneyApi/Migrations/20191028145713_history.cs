using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlashMoneyApi.Migrations
{
    public partial class history : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CardType",
                table: "UserTransaction",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CardType",
                table: "CardDetail",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TransactionHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    TransactionType = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Phone = table.Column<string>(nullable: true),
                    CardNumber = table.Column<string>(nullable: true),
                    CardName = table.Column<string>(nullable: true),
                    CardExpMonth = table.Column<string>(nullable: true),
                    CardExpYear = table.Column<string>(nullable: true),
                    CardType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionHistory", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionHistory");

            migrationBuilder.DropColumn(
                name: "CardType",
                table: "UserTransaction");

            migrationBuilder.DropColumn(
                name: "CardType",
                table: "CardDetail");
        }
    }
}
